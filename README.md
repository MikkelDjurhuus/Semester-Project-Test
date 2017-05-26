# Test Reflection
By Mikkel Djurhuus & Theis Rye
## Test-Driven-Development
 
Scrum and Test-Driven-Development(TDD) works well together. TDD can be used to shore up the “wild west”-mentality that can come with agile development. Reinforcing how important it is to build both the right product, but especially also build it right. 
 
This can be an issue, when you have built a product, all the consumers want, but the maintenance costs are staggering due to technical debt, of the scalability simply aren’t there due to you didn’t build the thing properly. 
 
These kinds of issues are cumbersome, annoying, expensive, staggering, expensive, and sometimes almost impossible to fix with hindsight. Allocating resources to fix issues created weeks, months, or maybe even years ago will require significantly more time and money to fix, compared to if it was fixed right away. 
 
In other words: It’s too late/expensive to try and fix your mistakes when they are discovered like this. You have to build the foundation of your system properly, to make sure it doesn’t come crashing down when too many users comes on. We want to use TDD to help avoid these issues.
 
TDD is also an effective tool, forcing the developer to think about the design and architecture of the code he’s about to write. Since the developer will be writing the test cases before the actual code, the developer must start by thinking about how the problem should be solved.
 
This will usually lead to a better architecture and improve the quality of the code. And that’s before getting any of the benefits from actually having an automated test suite at your disposal.
 
We discovered that the actual unit tests, and the automated test suite actually provided us with almost no value. Keep in mind, we still got value from TDD due to the aforementioned benefits. 
 
The system were too small and simple for the automated test suite to offer us any real value. We started out by creating a mock database class, and made all the queries work against that. With unit tests. After doing so, it was a simple matter of creating the rest of the architecture needed to implement the rest of the required functionality. At this time we had around 40~ unit tests which were failing, because we our methods for the actual databases didn’t return the expected results. It must be said, it was very satisfying seeing our tests succeed one after the other, when we implemented the functionality for the real databases.
 
Please keep in mind. We have no maintenance, expansion of the system, or the likes in the future. We almost certainly, never recommend not using the TDD methodology for software development. The future of the system is hard to predict, and it’s better to be safe than sorry.
 
## Continuous Integration
 
We used Travis for continuous integration. It worked well with our data mining system. Every time we pushed the project, it would run all our automated tests.
 
Although we did encounter issues with it when we tried to use it for our query/database system. We used a specific framework, “DevExtreme” for our ASP.NET front-end, which collided with Travis. We weren’t able to get them to together, and we chose not to spend too much time on it, since we still had to finish with the rest of the project. 
 
These kind of conflicts is unfortunately likely to happen when working with multiple 3rd party frameworks. It’s not always they can play nice with each other. We feel certain the issue could be addressed if we had had the time for it. We figured it was more important to finish the rest of the assignment, and leave this issue in the final project. Since it was already working with our data mining system.
 

## Database Testing
 
It was important to us, to know the integrity of our databases. To test for this we select certain entities from the database, and make sure they match up with their previous data. This sort of testing isn’t bulletproof. It’s a time and resource economy vs reward kind of calculation. What time and resource will this test cost us to make, and what is the potential reward?
 
We estimated creating a bigger test coverage of the potential database interactions would cost us way more time and resources, than the rewards were actually worth. Here is why: Our final system is a database read-only system. It never alters the data. Doing extensive delete/update/add/create statement testing, were simply unnecessary and a waste of time and resources. 
 
Since we know our system doesn’t edit the databases, we can be fairly sure that after the initial population of said databases, the data sets would never change again. Unless some unlike scenario were to happen, outside of our own systems interactions. 
 
Therefore we do the database integrity tests. We also tests the delete/update/add/create statements. We tests the statements we actually don’t use, as an extra level of safety, to make sure the database is acting as expected. If one of the statements we don’t actually use, starts to fail for unknown reasons, it’s worth looking into. What is the reason? Is something wrong with the database, have the data set been corrupted or what could have caused it ? 
 
In other words, the most important part of the statement test suite, is to ensure our databases integrity is intact, and the quality of them are known to us.
