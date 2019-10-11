The project has a "Master" branch and a "Develop" branch.

The "Master" branch is ONLY for completed, functioning, fully merged project code from "Develop".

"Develop" is the ONLY branch that should EVER be merged into "Master".

To avoid problems, merges into "Master" will only be done by 1 person.

Merges will flow as follows:

Issue Branch -> Develop -> Master

-> means "merges into"

Work will flow as follows:

Issue created
Team, Type, and Status labels applied
Branch created for issue
coding/working on issue.
coding complete (TL can review code for functionality, etc)
“Status: Completed - Review needed” label applied, to let QA/PM’s know code needs official quality & testing revision.
QA/PM applies the “status: under review” label, and reviews code for functionality & quality
Code passes review, QA/PM merges or labels “Status: completed, ready for merge”
OR Code fails review, gets “Status: Revision needed” label.
1. Merging

When your branch is ready for merging, Mark the related issue as “Status: Completed - review needed” The branch will then be reviewed by QA for completeness, testing, and quality.

If the branch fails, the label will be changed indicating it needs revision, notes will be included (in the issue comments) about the changes needed, and the coder who last worked on the issue will be contacted.

If the branch passes, it will be merged or marked as ready for merging, and/or discussed, depending on the magnitude of the changes, so the teams are aware of what’s going on.

2. When you create an issue

2.a Apply a "Team" Label

2.b Apply one or more of the "Type" Labels

2.c Apply a "Status" Label.

An issue should not have more than 1 status label.

3. When you work on an issue

3.a Assign it to yourself

3.b Change the "Status" label and other labels as necessary.

3.c Create a branch named "Issue#X" where "X" is the issue number of the issue you are working on. This way all the details about a branch can be easily found and people don't have to guess what a cryptically named branch is for.

Only one person should be assigned to an issue at a time. Issues can be passed between people, but it should only have 1 person's name assigned to it.

Comment on your issue frequently, share your thoughts, resources, what you tried, what worked and what didn't, and anything you learned that other's may benefit from.

4. Provide Useful commit messages

"safety commit" means you're about to do something risky and want to make sure you committed your work. This is fine. "did stuff" is not useful. What stuff? "Simplified movement controls and tidied code". This is acceptable. "fixed issue #23" will link the commit message to issue 23 in github, which is useful.
