# Proiect9_PC_UndaVerde :alien:

## Project Manager
- Andreea Carp
## DevTeam
- Andrei Chirap @AndreiChirap
- Adrian-Gabriel Balanescu @adrianB3
- Gabriel Bizdoc @GabiBVG
## DesignTeam

## DocsTeam

### How to open project and start working:
1. Download and install git from here: https://git-scm.com/downloads
2. Download and install VisualStudio2017 from here: http://ms.upt.ro/Blog/Category/DreamSpark
  - make sure to install this components with VS2017 ![VSInstall](https://i.imgur.com/xHyVEqY.png)
3. Clone repository
  - make a new folder wherever you want
  - inside that folder right-click and select 'Git Bash here'
    - if you installed git for the first time, you need to configure it - 
    type these commands with your credentials
      ```
        git config --global user.name "Your username"
        git config --global user.email johndoe@example.com
      ```
  - clone the repository - ` git clone https://github.com/adrianB3/Proiect9_PC_UndaVerde `
  - change into git directory - ` cd Proiect9_PC_UndaVerde `
  - check it - ` git remote -v `
4. Change to dev branch - ` git checkout dev ` - do not use master branch! 
5. Open the project with VisualStudio
6. Fetch changes made by others - ` git pull origin dev`
7. After you made your changes to the project, push those changes to the repository (on the dev branch or your own branch)
  ```
  git add .
  git commit -m "commit message"
  git push origin dev
  ```
8. That's all! :smiley:
     
