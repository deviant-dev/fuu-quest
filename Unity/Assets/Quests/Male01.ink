VAR Male01_has_food = false
VAR Male01_has_outfit = false


=== Male01Part01 ===
#left:Player
#right:Male01
#back:Forest
<h4> Part 1 </h4>
(Found near a specific creature location.)

+ <i>What's wrong?</i>

- Huh? Oh...
Well, I'm a little embarrased

+ [Creatures?] <i>Is it those creatures?</i>

- Yeah.. well...
The thing is, I want to raise a certain type of Fuu and they're the only creatures that will work.

+ <i>Yeah?</i>

- How am I supposed to get one of them to fertilize me when I can't even look at them without loosing it?
I must've cum 3 times already just by looking at them!
I wouldn't last one minute out there...

+ [I can help.] <i>Oh, I see...Maybe there is some way I could help?</i>

- You would help me?!
But how?

+ [Get Food?] <i>I could get the food for you!</i>
    You would do that?! Thank you so much!
    -> Male01Part02
+ [Practice?] <i>What if you had some more practice?</i>

- What do you mean?

+ [Let's Fuck!] <i>You know.. Like... We could fuck for a while...</i>
<i>If I dressed up, it would make for good practice for the real thing.</i>

- Yeah... I mean, it's worth a try, right?

+ [Yes!] <i>I think so!</i>
<i>Let me find an outfit and I'll be back for you.</i>
<i>Just be ready!</i>

- Okay... I'll wait right here then. -> Male01Part03a


=== Male01Part02 ===
<h4> Part 2 </h4>

- (start)

+ { not Male01_has_food } (Gather Food)
    ~ Male01_has_food = true
+ { Male01_has_food } (Eat Food) 
    ~ Male01_has_food = false
+ \(Approach Student) -> approach

- -> start

= approach
{
    - Male01_has_food:
        + [Give Food] <i>I'm starting to understand your fixation with these creatures... Here you go!</i>
        -> give_food
    - else:
        Did you get some of the food?
        
        + [No] None yet, sorry. -> start
}

= give_food

~ Male01_has_food = false

        
- Wow! You were amazing!
My Fuu will be very grateful!
        
+ [My Pleasure] <i>It was my pleasure. Really...</i>

->->


=== Male01Part03a ===
<h4> Part 3a </h4>

- (start)

+ {not Male01_has_outfit} [Put On Outfit] <i>You put on the outfit.</i>
    ~ Male01_has_outfit = true
+ {Male01_has_outfit} [Take Off Outfit] <i>You take off the outfit.</i>
    ~ Male01_has_outfit = false
+ [Approach] <i>You approach the student once again.</i> -> approach

- -> start

= approach
- About this plan... I'm not so sure I-

+ {not Male01_has_outfit} [BRB] <i>Give me a little while longer!</i>
    -> start
+ {Male01_has_outfit} [Ready!!] You ready?

- Gah! You look... Oh boy...

+ <i>I know, pretty silly right?</i>

- \*GULP*

(Sex: Cums almost immediately)

+ [Nice] <i>You did really well!</i>

- You think?

+ <i>Well...[] I mean, there's always room for improvement.</i>

- Oh...

+ [Keep trying?] <i>Ah! Don't make that face! We can keep practicing and you'll get better!</i>

Time passes...

-> Male01Part03b


=== Male01Part03b ===
<h4> Part 3b </h4>

- (start)

+ {not Male01_has_outfit} [Put On Outfit] <i>You put on the outfit.</i>
    ~Male01_has_outfit = true
+ {Male01_has_outfit} [Take Off Outfit] <i>You take off the outfit.</i>
    ~Male01_has_outfit = false
+ [Approach] <i>You approach the student once again.</i> -> approach

- -> start

= approach

{
    - not Male01_has_outfit:
        No outfit this time?
        + <i>Oh shoot! I'll be right back!</i> -> Male01Part03b
    - else:
        -> continue
}

= continue

+ <i>Ready for more practice?!</i>

- As I'll ever be.

+ <i>Show me[] what you got\~</i>

- \(Sex: Lasts much longer before cumming.)

+ <i>Gosh, you really have gotten better...</i>
+ <i>My legs are shaking after that one.</i>

- Really? Wow!

+ <i>Yeah, really\~</i>
<i>What do you say we go one more round to be sure you're ready for the real thing?</i>

- Yeah, sure
Just give me a moment to gather myself..

-> Male01Part03c


=== Male01Part03c ===
<h4> Part 3c </h4>

- (start)

+ {not Male01_has_outfit} [Put On Outfit] <i>You put on the outfit.</i>
    ~Male01_has_outfit = true
+ {Male01_has_outfit} [Take Off Outfit] <i>You take off the outfit.</i>
    ~Male01_has_outfit = false
+ [Approach] <i>You approach the student.</i> -> approach

- -> start

= approach
- {
    - not Male01_has_outfit:
        I think you might be forgetting something...
        + <i>Gah! Be right back!</i> -> Male01Part03b
    - else:
        -> continue
}

= continue

+ <i>Hey hot stuff!</i>
<i>I'm ready if you are\~</i>

- When you show up looking like that, how could I not be~

(Sex)

+ <i>You've really gotten the hang of this[] haven't you!</i>
<i>I was ready to burst from the beginning~</i>
    
- All thanks to you really~

+ <i>You were dedicated[] and practiced a lot!</i>

- \*Blush*

+ [It's time] <i>Well it's about time you put your new skills to the test!</i>

- You're right...
I can finally take on those lewd creatures and get the Fuu food!
I'm getting so many butterflies~
Thanks again and see you around!

+ Have fun\~

->->
