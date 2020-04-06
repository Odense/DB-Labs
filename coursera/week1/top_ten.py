import sys
import json
from collections import Counter


def main():
    tweet_file = open(sys.argv[1])
    hashtags_collection = {}
    for line in tweet_file:
        tweet = json.loads(line)
        if "entities" in tweet:
            entities = tweet["entities"]
            hashtags_per_line = entities["hashtags"]

            for hashtag in hashtags_per_line:
                text = hashtag["text"]
                if text in hashtags_collection.keys():
                    hashtags_collection[text] += 1
                else:
                    hashtags_collection[text] = 1

    ordered_hashtags = Counter(hashtags_collection)
    top_ten = ordered_hashtags.most_common(10)
    for top in top_ten:
        print(top[0] + " " + str(top[1]))


if __name__ == '__main__':
    main()
