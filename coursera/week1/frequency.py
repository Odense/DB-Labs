import sys
import json
import re


def main():
    tweet_file = open(sys.argv[1])
    words_counter = 0
    words = {}
    for line in tweet_file:
        tweet = json.loads(line)
        if "text" in tweet:
            tweet['text'].encode('ascii', 'replace')
            text = tweet["text"]
            text = text.rstrip("\n")
            splitted_text = re.split(r"[\s\.\,\?\:\!\n]+", text)

            words_counter += len(splitted_text)

            for word in splitted_text:
                if word in words and word != '':
                    words[word] += 1
                elif word != '':
                    words[word] = 1

    for word in words.keys():
        print(word + " " + str(round(words[word] / words_counter, 4)))


if __name__ == '__main__':
    main()
