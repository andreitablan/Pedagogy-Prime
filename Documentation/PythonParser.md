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
* We need to try to make sure abreviations are also considered. 
* This will need to be integrated within an API

## Implemenation brainstorming

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

