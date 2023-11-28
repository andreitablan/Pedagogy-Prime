from nltk.corpus import wordnet
from nltk import pos_tag, word_tokenize
from flask import Flask, request, jsonify
import PyPDF2
import requests

hardcoded_words = ['course', 'teacher', 'student'] # Add more if needed


app = Flask("Parser")

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

if __name__ == '__main__':
    app.run(debug=True)
