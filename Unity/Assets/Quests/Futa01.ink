VAR Futa01_has_mushroom = false

=== Futa01Part01 ===
<h4> Part 1 </h4>
(Seen someplace dark and shady)

Hey there\~ Mind helping me out?

+ <i>Sure, what’s up?</i>
+ <i>Sorry, I’m in a rush!</i> -> leave

- Well, I’m looking for something.
A kind of mushroom...

+ <i>[In the dark?] Hmm, I don’t expect we’ll find much over here in the dark.</i>

- Actually, this mushroom should be easiest to find in the dark. It’s bioluminescent!

+ <i>A glowing mushroom?</i>

- A Starry Night Cap to be exact.
They are a wonderfully useful mushroom that let off a beautiful deep blue glow.
I’ve been told that finding their clusters in a dark cave is just like looking up at starry night sky!
I’d imagine that’s where the name comes from.

+ <i>Sounds amazing![] Any luck so far?</i>

- They’re supposed to be native to this area but I haven't seen any since I’ve been out here...

+ <i>Maybe I could help you find some.</i>
+ <i>Wish you luck. I better go!</i> -> leave

- That sure is nice of you.
There’s no need for you to strain yourself but if you happen to see any...

+ <i>Absolutely![] If I see any, I’ll bring them right back to you.</i>

- Great, thank you!
I’ll be here if you manage to find any.

-> Futa01Part02

= leave

Okay, see you. 
->->

=== Futa01Part02 ===
<h4> Part 2 </h4>
(Seen someplace dark and shady)

- (start)

+ {Futa01_has_mushroom} (Drop mushrooms)
~ Futa01_has_mushroom = false
+ {not Futa01_has_mushroom} (Find mushrooms)
~ Futa01_has_mushroom = true
+ \(Approach Futa) -> approach

- -> start

= approach
{
    - Futa01_has_mushroom:
        Is it me or are your pockets glowing?
        + <i>I have some mushrooms for you\~</i> -> give_mushrooms
    - else:
        Anything?
        + <i>Nah, nothing yet...</i> -> start
}

= give_mushrooms
~ Futa01_has_mushroom = false

That’s great! Let me see.

+ \(Give Starry Night Caps)

- They are great! Where did you find them?

+ <i>They were in a dark place[] just like you said! The cave over that way.
- Perfect, so there’s more there?</i>

+ <i>Yeah, lots of ‘em.</i>

- Oh I’m all jealous now.
I’d like to see the Starry Night Caps for myself...

+ <i>It’s no trouble once you know where to look!</i>
<i>Why don’t you head over to the cave? It really is a beautiful sight.</i>

- Absolutely, I’ll go there now.
I appreciate you helping me out so much!
These Starry Night Caps really are invaluable to me.

+ <i>[No problem.] It’s really no problem! I had fun exploring.</i>

- Hopefully I see you again soon\~

-> Futa01Part03

=== Futa01Part03 ===
<h4> Part 3 </h4>

(Found in the caves with the mushrooms)

+ <i>Glad you found them alright!</i>

- Yeah, it’s been nice to take in the sight...
While you’re here, could you help me with one other thing?

+ <i>Sure, what is it?</i>

- Well, these mushrooms... Th-they don’t just glow you know.
They also help me grow my very own dick...

+ <i>\*Gulp*</i>

- I wanted to try this before this trip was over and thanks to you, I can!
When I’m collecting Fuu food from the creatures, it’s always me getting fucked.
Don’t get me wrong, it’s a lot of fun\~
But watching those big engorged dicks plunging deep inside me. And seeing how much they cum...
It makes me wonder what it must feel like.

+ <i>And you need help?</i>

- After I eat this mushroom, I’ll grow a dick temporarily...
But I’ll need to test it out first in case it goes wrong.
I wouldn't want it to do something unexpected while I’m out having sex right?
Some of the magical creatures can be pretty judgemental...
Would you be willing to help me out one more time?

+ <i>Absolutely, yes.</i>
+ <i>[Actually...] Maybe I’m not the best person to ask for this… Sorry.</i> -> leave

- \(NPC dominant sex encounter until climax)
I would have never imagined having a dick would feel this good.

+ <i>[Wow] Merlin have mercy! That was outstanding!</i>

- Now that this thing is approved, I’m ready to start exploring the woods\~
See you around?

+ <i>See you!</i>

->->

= leave
Hmm… Alright.
Come back here if you change your mind\~
->->