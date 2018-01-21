VAR Male02_ingredient = false


=== Male02Part01 ===
#left:Player
#right:Male02
#back:Forest
<h4> Part 1 </h4>
(Found somewhere in the woods)

+ <i>What's with that potion? Did you make it?</i>

- OH! Yes! I've created a solution for producing Fuu food myself.

Now I won't need to search the woods collecting bumps and bruises in order to find the food.

+ <i>Are you sure that's safe?</i>

- Yeah! It's perfectly safe!
Probably...
I haven't tried it yet.

+ <i>What are you waiting for?</i> -> why_wait
+ <i>[Ask the professor?]Maybe you should have the professor look at it first.</i>

- NO! Don't tell the professor!

+ <i>What? Why?</i>

- It's kind of... Against the rules to make potions outside of potions class...
And I didn’t exactly grow all these ingredients myself...
I had to use some school resources.

+ <i>Okay, okay![] Calm down. I won't say anything.</i>
+ <i>You're in trouble.</i>
#left:Player
#right:Futa03
#back:Greenhouse
<h3> Futa 3 </h3>
<h4> Part 2 </h4>
-> Futa03Part02.potion

- \*Sigh* Thank you.
I would really be in trouble if the professor knew.
She still has it out for me... Ever since she discovered it was me transfiguring her cat.

+ <i>You what?</i>

- Gah! I mean- Forget I said anything!

+ <i>Hmm... Okay...</i>
<i>So are you going to test out your potion?</i>

- (why_wait)

Well, I still need one last ingredient for the potion to work.
I haven't been able to get my hands on any Succubus Milk...

+ <i>I bet I could find that ingredient for you!</i>

- Wow! Really? That would be great!

+ <i>Sure, wait here.</i>

- Thank you! -> Male02Part02


=== Male02Part02 ===
<h4> Part 2 </h4>

- (start)

+ {Male02_ingredient} (Drop ingredient.)
    ~ Male02_ingredient = false
+ {not Male02_ingredient} (Find ingredient.)
    ~ Male02_ingredient = true
+ \(Approach student.) -> approach

- -> start

= approach

{ Male02_ingredient: -> has_ingredient }

- Any luck?

+ <i>No[ :(] luck so far... Sorry.</i>

Let me know when you've found it, won't you?

-> start

= has_ingredient
How did you manage?

+ [Found it!] <i>Yeah, Here it is!</i>
~ Male02_ingredient = false

- Perfect! Let's give it a try then.
\*Gulp*

+ <i>How does it feel?</i>

- Whoa...
I feel...
Oh no!

(He runs away to where the player can't follow)

-> Male02Part03


=== Male02Part03 ===
<h4> Part 3 </h4>

(You return to the same place.)
(His genitals have enlarged quite considerably)

+ <i>What happened?!</i>

- Isn't it obvious?!
The potion!
I never had to cum so bad in my life! It feels like I'll burst!

+ <i>So? Then cum, why don't you?!</i>

- Don't you think I'm trying?!
I'm so close to the edge but I can't get off!
Got any ideas?!

+ <i>[I can fix this...] Okay, okay... I think I have an idea. Just... Don't move.</i>
+ [<i>I better get the professor!</i>]
<h3> Futa 3 </h3>
<h4> Part 2 </h4>
-> Futa03Part02.potion

- <i>(Sex: Milking his dick while eating his ass)</i>

Lucky you showed up!
Thought I would explode.

+ <i>You kinda did...</i>

- I've never cum that hard in my life...

+ <i>Well it looks like your stuff has gone back to normal...</i>
- <i>Good thing too; I'm not sure what would have happened if we didn't release that pressure...</i>

I don't want to think about it...

+ <i>So, that potion[...] of yours... Do you mind if I...?</i>

- Uh... Sure, I guess so. I don't think it agrees with me...

+ <i>Thanks!</i> ->->