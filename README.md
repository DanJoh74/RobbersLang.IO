This repo is an attempt at the following challenge, using an "IO approach"...

# The Challenge!

Do not google it, and put your answer on Github and share the link with me. I will crown a winner in the next technomail!

## Description
When we Swedes are young, we are taught a SUPER-SECRET language that only kids know, so we can hide secrets from our confused parents. This language is known as "Rövarspråket" (which means "Robber's language", more or less), and is surprisingly easy to become fluent in, at least when you're a kid. 
Rövarspråket is not very complicated: you take an ordinary word and replace the consonants with the consonant doubled and with an "o" in between. So the consonant "b" is replaced by "bob", "r" is replaced with "ror", "s" is replaced with "sos", and so on. Vowels are left intact. It's made for Swedish, but it works just as well in English.
Your task today is to write a program that can encode a string of text into Rövarspråket.

## Formal inputs & outputs
### Input
You will receive one line of input, which is a text string that should be encoded into Rövarspråket.
### Output
The output will be the encoded string.
A few notes: your program should be able to handle case properly, which means that "Hello" should be encoded to "Hohelollolo", and not as "HoHelollolo" (note the second capital "H").
Also, since Rövarspråket is a Swedish invention, your program should follow Swedish rules regarding what is a vowel and what is a consonant. The Swedish alphabet is the same as the English alphabet except that there are three extra characters at the end (Å, Ä and Ö) which are all vowels. In addition, Y is always a vowel in Swedish, so the full list of vowels in Swedish is A, E, I, O, U, Y, Å, Ä and Ö. The rest are consonants.
Lastly, any character that is not a vowel or a consonant (i.e. things like punctuation) should be left intact in the output.

## Example inputs
### Input 1
> Jag talar Rövarspråket!
### Output 1
> Jojagog totalolaror Rorövovarorsospoproråkoketot!
### Input 2
> I'm speaking Robber's language!
### Output 2
> I'mom sospopeakokinongog Rorobobboberor'sos lolanongoguagoge!

## Challenge inputs
### Input 1
> Tre Kronor är världens bästa ishockeylag.
### Input 2
> Vår kung är coolare än er kung.
### Bonus
Make your program able to decode a Rövarspråket-encoded sentence as well as encode it.

Till next time!