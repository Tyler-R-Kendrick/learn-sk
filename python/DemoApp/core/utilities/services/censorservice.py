import re

class CensorService:
    def __init__(self, *banned_phrases):
        self._banned_phrases = banned_phrases
        self._regexes = []
        self._censored_text = '[Censored]'
        self._pattern_template = r'\b({0})(s?)\b'

        for banned_phrase in banned_phrases[0]:
            pattern_text = self._pattern_template.format(banned_phrase)
            self._regexes.append(re.compile(pattern_text, re.IGNORECASE))

    def transform(self, input_text):
        for regex in self._regexes:
            input_text = regex.sub(self._censored_text, input_text)
        return input_text


    def reject(self, input_text) -> bool:
        for regex in self._regexes:
            if regex.search(input_text):
                raise ValueError('Input contains banned phrase')

 