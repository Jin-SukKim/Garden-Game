Git Standards
Eamonn edited this page 1 hour ago · 1 revision
The project has a "Master" branch and a "Develop" branch.

The "Master" branch is ONLY for completed, functioning, fully merged project code from "Develop".

"Develop" is the ONLY branch that should EVER be merged into "Master".
Do NOT work directly on the develop branch. 

All work is to be done on branches branched off of develop. 
Do not work on anything without creating an issue for it, and then creating a branch for that issue. 

1. When you create an issue
2.a Apply a "Team" Label
2.b Apply one or more of the "Type" Labels
3.c Apply a "Status" Label.
An issue should not have more than 1 status label.

2. When you work on an issue
2.a Assign it to yourself
2.b Change the "Status" label and other labels as necessary.
2.c Create a branch named "Issue#X" where "X" is the issue number of the issue you are working on. This way all the details about a branch can be easily found and people don't have to guess what a cryptically named branch is for.
Only one person should be assigned to an issue at a time. Issues can be passed between people, but it should only have 1 person's name assigned to it.

Comment on your issue frequently, share your thoughts, resources, what you tried, what worked and what didn't, and anything you learned that other's may benefit from.

3. Provide Useful commit messages
"safety commit" means you're about to do something risky and want to make sure you committed your work. This is fine. "did stuff" is not useful. What stuff? "Simplified movement controls and tidied code". This is acceptable. "fixed issue #23" will link the commit message to issue 23 in github, which is useful.
