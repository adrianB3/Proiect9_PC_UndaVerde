### Some resources to get you started on C# and Multithreading in .Net

 - http://www.tutorialsteacher.com/csharp/csharp-tutorials -- you don't need to memorize everything from here, read just to get an idea
 - regarding parallel tasks in .Net we have two options:
    - either we use direct OS threads as described in here: http://jonskeet.uk/csharp/threads/ --> this approach is simpler to learn in 
    the beginning, but is quite limited
    - or tasks which are like threads but with a higher level of abstraction as described in these videos: https://uptro29158-my.sharepoint.com/:f:/g/personal/adrian_balanescu_student_upt_ro/Ej6haOjlamhBiDqw_7AHmcsBIExlE0SC8kAJdRjA4qMiLg
    or this: http://mapmf.pmfst.unist.hr/~tdadic/Apress.Pro.dotNET.4.Parallel.Programming.in.CSharp.May.2010.pdf
    --> task provide more functionality and are more efficient and are the default way of doing parralel programs in modern C#
    
  - for optimization of the semaphores time we will use a genetic algorithm as described in here: https://www.codeproject.com/Articles/873559/Implementing-Genetic-Algorithms-in-Csharp
  
  - for animating the cars, check the documentation: https://docs.microsoft.com/en-us/dotnet/framework/wpf/graphics-multimedia/animation-overview
  
#### we need to discuss what approach to use ASAP
 
 
 
### DevTeam - until sunday you need to try to perform the following tasks:
 - read the code written so far - Ask questions if you don't understand something!
 - create an intersection class that will contain the necessary SemaphoreUI elements and the necessary methods
 - add intersection objects to the SemaphoreSystem class and try to think of a method to synchronize them
 - in order to make the intersection you need to find a way to rotate a SemaphoreUI image and a method to synchronize the SemaphoreUI elements from within the intersection
 - add comments to main functions
 - think of the necessary steps for a user to make in order to initialize a simulation
 
 #### The code doesn't need to be perfect, just try to write something and we will discuss it!
