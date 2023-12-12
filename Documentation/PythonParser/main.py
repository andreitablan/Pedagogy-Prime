from nltk.corpus import wordnet
from nltk import pos_tag, word_tokenize
from flask import Flask, request, jsonify
from flask_cors import CORS
import PyPDF2
import requests

hardcoded_words = ['course', 'teacher', 'student'] # Add more if needed


app = Flask("Parser")
CORS(app)

def get_synonyms(word):
    synonyms = set()
    for syn in wordnet.synsets(word):
        for lemma in syn.lemmas():
            synonyms.add(lemma.name())
    return synonyms

def analyze_text(description, pdf_url):
    description_tokens = word_tokenize(description.lower())
    text_tokens = word_tokenize(extract_text_from_pdf(pdf_url))

    description_nouns = set()
    for word, pos in pos_tag(description_tokens):
        if pos.startswith('N') and word not in hardcoded_words:
            description_nouns.add(word)

    if not description_nouns:
        print("No relevant keywords found in the description.")
        return

    good_keywords = []
    bad_keywords = []

    for keyword in description_nouns:
        synonyms = get_synonyms(keyword)
        if any(synonym in text_tokens for synonym in synonyms):
            good_keywords.append(keyword)
        else:
            bad_keywords.append(keyword)

    total_words = len(good_keywords) + len(bad_keywords)

    coverage = (len(good_keywords) / total_words) * 100

    result = {
        'coverage': coverage,
        'good_keywords': good_keywords,
        'bad_keywords': bad_keywords
    }

    return result


def find_common_words_in_next_course(words, pdf_url):
    print(words)
    text_tokens = word_tokenize(extract_text_from_pdf(pdf_url))
    result = set()

    for word in words:
        if word in text_tokens:
            result.add(word)

    return result


def extract_text_from_pdf(pdf_url):
    response = requests.get(pdf_url)
    with open('temp.pdf', 'wb') as f:
        f.write(response.content)

    text = ''
    with open('temp.pdf', 'rb') as pdf_file:
        pdf_reader = PyPDF2.PdfReader(pdf_file)
        num_pages = len(pdf_reader.pages)
        for page_num in range(num_pages):
            page = pdf_reader.pages[page_num]
            text += page.extract_text()
            text = text.lower()

    return text



@app.route('/check-course', methods=['POST'])
def check_pdf():
    data = request.get_json()
    firebase_link = data.get('firebase_link')
    description = data.get('description', "")

    result = analyze_text(description, firebase_link)

    return jsonify(result)

@app.route('/check-multiple-courses', methods=['POST'])
def check_multiple_courses():
    data = request.get_json()
    firebase_links = data.get('firebase_links', [])
    descriptions = data.get('descriptions', [])

    results = []

    for link, description in zip(firebase_links, descriptions):
        result = analyze_text(description, link)
        results.append(result)

    return jsonify(results)

@app.route('/check-course-v2', methods=['POST'])
def compare_courses():
    data = request.get_json()
    firebase_link = data.get('firebase_link')
    description = data.get('description', "")
    course2_link = data.get('course2_link')

    result_course1 = analyze_text(description, firebase_link)

    if course2_link != "":
        common_keywords = list(set(result_course1['good_keywords']) | set(result_course1['bad_keywords']))
        found_words = find_common_words_in_next_course(common_keywords, course2_link)

        result_course1['common_keywords'] = list(found_words)

    return jsonify(result_course1)


if __name__ == '__main__':
    app.run(debug=True)
