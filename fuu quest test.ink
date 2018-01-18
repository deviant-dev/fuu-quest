#Male 1: Part 1

#Seen near a specific creature location

VAR has_food = false
VAR has_outfit = false

-> Male1Part1


=== Male1Part1 ===

* <i>What's wrong?</i>

- Huh? Oh...
    Well, I'm a little embarrased

* [Creatures?] <i>Is it those creatures?</i>

- Yeah.. well...
The thing is, I want to raise a certain type of Fuu and they're the only creatures that will work.

* <i>Yeah?</i>

- How am I supposed to get one of them to fertilize me when I can't even look at them without loosing it?
I must've cum 3 times already just by looking at them!
I wouldn't last one minute out there...

* [Help] <i>Oh, I see...Maybe there is some way I could help?</i>

- You would help me?!
But how?

* [Get Food?] <i>I could get the food for you!</i>
    You would do that?! Thank you so much!
    -> Male1Part2
* [Practice?] <i>What if you had some more practice?</i>

- What do you mean?

* [Let's Fuck!] <i>You know.. Like... We could fuck for a while...</i>
<i>If I dressed up, it would make for good practice for the real thing.</i>

- Yeah... I mean, it's worth a try, right?

* [Yes!] <i>I think so!</i>
<i>Let me find an outfit and I'll be back for you.</i>
<i>Just be ready!</i>

- Okay... I'll wait right here then. -> Male1Part3a


=== Male1Part2 ===

* { not has_food } [Gather Food] <i>You gather up some food.</i>
    ~ has_food = true
    -> Male1Part2
+ [Approach Creature] <i>You approach the creature once more.</i>

{
    - has_food:
        * { has_food } [Give Food] -> give_food
        
    - else:
        Did you get some of the food?
        
        * [No] None yet, sorry. -> Male1Part2
}

= give_food

~has_food = false

<i>I'm starting to understand your fixation with these creatures... Here you go!</i>
        
- Wow! You were amazing!
My Fuu will be very grateful!
        
* [My Pleasure] <i>It was my pleasure. Really...</i>

-> END


=== Male1Part3a ===

+ {not has_outfit} [Put On Outfit] <i>You put on the outfit.</i>
    ~has_outfit = true
    -> Male1Part3a
+ {has_outfit} [Take Off Outfit] <i>You take off the outfit.</i>
    ~has_outfit = false
    -> Male1Part3a
+ [Approach] <i>You approach the creature once again.</i>

- About this plan... I'm not so sure I-

+ {not has_outfit} [BRB] <i>Give me a little while longer!</i>
    -> Male1Part3a
* {has_outfit} [Ready!!] You ready?

- Gah! You look... Oh boy...

* <i>I know, pretty silly right?</i>

- \*GULP*

<i>Sex: Cums almost immediately</i>

* [Nice] <i>You did really well!</i>

- You think?

* <i>Well...[] I mean, there's always room for improvement.</i>

- Oh...

* [Keep trying?] <i>Ah! Don't make that face! We can keep practicing and you'll get better!</i>

Time passes...

-> Male1Part3b


=== Male1Part3b ===

+ {not has_outfit} [Put On Outfit] <i>You put on the outfit.</i>
    ~has_outfit = true
    -> Male1Part3b
+ {has_outfit} [Take Off Outfit] <i>You take off the outfit.</i>
    ~has_outfit = false
    -> Male1Part3b
+ [Approach] <i>You approach the creature once again.</i>

{
    - not has_outfit:
        No outfit this time?
        + <i>Oh shoot! I'll be right back!</i> -> Male1Part3b
    - else:
        -> continue
}

= continue

* <i>Ready for more practice?!</i>

- As I'll ever be.

* <i>Show me[] what you got~</i>

- <i>Sex: Lasts much longer before cumming.</i>

* <i>Gosh, you really have gotten better...</i>
    <i>My legs are shaking after that one.</i>

- Really? Wow!

* <i>Yeah, really~</i>
<i>What do you say we go one more round to be sure you're ready for the real thing?</i>

- Yeah, sure
Just give me a moment to gather myself..
Time passes...

-> Male1Part3c


=== Male1Part3c ===

+ {not has_outfit} [Put On Outfit] <i>You put on the outfit.</i>
    ~has_outfit = true
    -> Male1Part3c
+ {has_outfit} [Take Off Outfit] <i>You take off the outfit.</i>
    ~has_outfit = false
    -> Male1Part3c
+ [Approach] <i>You approach the creature.</i>

- {
    - not has_outfit:
        I think you might be forgetting something...
        + <i>Gah! Be right back!</i> -> Male1Part3b
    - else:
        -> continue
}

= continue

* <i>Hey hot stuff!</i>
<i>I'm ready if you are~</i>

- When you show up looking like that, how could I not be~

<i>Sex</i>

* <i>You've really gotten the hang of this haven't you!</i>
    <i>I was ready to burst from the beginning~</i>
    
- All thanks to you really~

* <i>You were dedicated and practiced a lot!</i>

- *Blush*

* <i>Well it's about time you put your new skills to the test!</i>

- You're right...
I can finally take on those lewd creatures and get the Fuu food!
I'm getting so many butterflies~
Thanks again and see you around!

* Have fun~
-> END
