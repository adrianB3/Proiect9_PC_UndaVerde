<p>Hello design team!</p>

#### Task #1: -->Deadline 25 Apr<--
  - Read carefully the project requirments listed in the /ProjectRequirments folder
  - Everybody from the designTeam needs to design (using photoshop or by hand) an interface for the application in this framework
    ![Imgur](https://i.imgur.com/MoLOF0c.png)
    - Add a short description for each element!
  - Upload your desing to /ideas/design folder
    - Make sure to add a commit message and a description for your interface design
    
    ![Imgur](https://i.imgur.com/5xhwqgw.png)
    ![Imgur](https://i.imgur.com/rNlT0kq.png)
    
  - Next, we will discuss the uploaded designs
 
  
  #### Task #2:
  - Read about XAML from here: https://www.tutorialspoint.com/xaml/index.htm
  - Your task is to design the main UI elements for the project in XAML
  - The elements are: semaphore for cars, semaphore for trains, camera, sensor
  ##### Example:
  1. Make a new WPF project in VS -- File/New/Project -- Visual C#/WPF App(.NET Framework)
  2. Edit the MainWindow.xaml file
    - Inside `<Grid>...</Grid>` write the XAML code for the UI element
    - For example to create a shape check the documentation here:
    https://docs.microsoft.com/en-us/dotnet/framework/wpf/graphics-multimedia/shapes-and-basic-drawing-in-wpf-overview
    and here
    https://www.tutorialspoint.com/wpf/wpf_2d_graphics.htm
    - An ellipse would look like this: 
      ```
      <Grid>
        <Ellipse
          Fill="Yellow"
          Height="100"
          Width="200"
          StrokeThickness="2"
          Stroke="Black"/>
      </Grid>
      ```
  3. Upload the code in a new file containing only the XAML code for the UI elements.
