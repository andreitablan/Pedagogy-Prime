# Python Parser

## Pre-requisites

Python needs to be installed [download](https://www.python.org) \
A new project needs to be created. Within the project open a terminal and run the following command

```
pip install nltk
```

nltk is a suite of libraries for natural language processing. To have access to what we need we'll also need to download several things. \
Within a python file add the following lines of code

```python
import nltk
nltk.download('wordnet')
nltk.download('punkt')
nltk.download('averaged_perceptron_tagger')
```

This will download the needed datasets for the operations we will have to do over our inputs.

## Main Idea

* The parser will need some input. 
  * This input will come from the Subject Overview document (additional parsing) or a description for each project
  * The course which is a PDF/Word format => extract words
* The output will consist of the found topics, those that are not found and a coverage percentage.
    * For example, if Course 1 is about SOLID and the actual pdf/word only has info about SOL our output will be something like this(of course, JSON format):
        *   | Found  | Not found | Coverage |
            | ------------- |  ------------- |:-------------:|
            | S      | I    |       60% |
            | O      | D    |
            | L      | 
* We will parse and search information about synonyms for the relevant topics
* We need to try to make sure abbreviations are also considered. 
* This will need to be integrated within an API

## Implemenation brainstorming

### Parser
The description will basically be some phrases about what is discussed in the course. The main idea would probably be to separate the nouns in the phrases because most likely they are the topics. We should then take out from these nouns the words which are common for describing a course. EX: Course, Teacher, Student etc.

This can be done with the nltk `pos_tag` function. \
Example of usage: 
```python
from nltk import pos_tag, word_tokenize
tokens = word_tokenize(text.lower())
print(pos_tag(tokens))
```
For the input being 'This course is about c++ and python.' the result will look something like this: [('this', 'DT'), ('course', 'NN'), ('is', 'VBZ'), ('about', 'IN'), ('c++', 'NN'), ('and', 'CC'), ('python', 'NN'), ('.', '.')]

Thus we have a list of tokens and their type(noun, verb, etc). So we can now just separate the nouns and continue the parsing.

**Further improvement will be needed in the coming weeks to define a solid understanding of what words nouns should be taken into consideration**
* As I said, we can have words that are common when describing a course(Course, Teacher, Student) -> Is this needed? How should we proceed? At the moment these are hardcoded and taken out from the final topics list
* We can have 2 nouns which are connected and describe the same thing, but one may be covered and one may not, which is wrong since they're describing the same thing in the context
    * Ex: During testing of different scenarios we came upon this issue:
        * Description: "This course focuses on basketball strategies and player techniques."
        * Output: techniques -> good, player -> bad. In this scenario both should be good

### Synonyms 

For synonyms we can use `nltk` with the `synsets` method which can describe a word using some lemmas thus getting access to synonyms and not only. More information can be found [in the nltk documentation](https://www.nltk.org/howto/wordnet.html)

What we're trying to do can pretty much be covered in few lines of code

```python
def get_synonyms(word):
    synonyms = set()
    for syn in wordnet.synsets(word):
        for lemma in syn.lemmas():
            synonyms.add(lemma.name())
    return synonyms
```

Thus we get a list of the synonyms for any given word we give as input. We can then try to search the courses for any words that may be synonyms to the relevant words from the course description.

### PDF/Word Parsing(TBD)

Here will be some info if we will be parsing the documents with python.

### Abbreviations(TBD/To be tested)

Here will be some info once Abbreviations cases will be handled/tested.

### Integration within an API(TBD)

We will need to integrate this within an API(Django/Flask probably) so we can give input and get some output in JSON format from our main application. 


## Code

I've added some sample code for testing and improvement in the coming weeks.