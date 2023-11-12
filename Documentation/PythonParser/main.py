from nltk.corpus import wordnet
from nltk import pos_tag, word_tokenize

hardcoded_words = ['course', 'teacher', 'student'] # Add more if needed

def get_synonyms(word):
    synonyms = set()
    for syn in wordnet.synsets(word):
        for lemma in syn.lemmas():
            synonyms.add(lemma.name())
    return synonyms

def analyze_text(description, text):
    description_tokens = word_tokenize(description.lower())
    text_tokens = word_tokenize(text.lower())

    description_nouns = set()
    for word, pos in pos_tag(description_tokens):
        if pos.startswith('N') and word not in hardcoded_words:
            description_nouns.add(word)

    if not description_nouns:
        print("No relevant keywords found in the description.")
        return

    common_keywords = description_nouns.intersection(set(text_tokens))
    coverage = len(common_keywords) / len(description_nouns) * 100

    for keyword in description_nouns:
        synonyms = get_synonyms(keyword)
        if text.lower() in synonyms or any(synonym in text.lower() for synonym in synonyms):
            print(f"good => {keyword}")
        else:
            print(f"bad => {keyword}")

    print(f"Coverage: {coverage:.2f}%")

if __name__ == "__main__":
    description = "This course is about c++ and python."
    text = "Python is a high-level, general-purpose programming language. Its design philosophy emphasizes code readability with the use of significant indentation. Python is dynamically typed and garbage-collected. It supports multiple programming paradigms, including structured, object-oriented, and functional."
    analyze_text(description, text)

    description = "This course covers cellular biology and genetics."
    text = "Genetics is the study of genes and heredity. Cells are the basic building blocks of living organisms."
    analyze_text(description, text)

    description = "This course focuses on basketball strategies and player techniques."
    text = "Basketball is a fast-paced sport played between two teams. Players aim to score points by shooting the ball through the opponent's hoop."
    analyze_text(description, text)

    description = "This course delves into algorithms and data structures using Python."
    text = "Python is a versatile programming language used for various applications. Algorithms and data structures are fundamental concepts in computer science."
    analyze_text(description, text)
