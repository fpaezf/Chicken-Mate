# VB.NET Chicken Mate desktop pet 

<img width="100%" src="https://i.postimg.cc/26kpKmZk/11.png">

<img alt="Windows" src="https://img.shields.io/badge/-Windows-0078D6?style=flat&logo=windows&logoColor=white"/> <img alt="NET" src="https://img.shields.io/badge/-Visual%20Basic-blue?style=flat&logo=.net&logoColor=white"/>

Translate this page to english: https://bit.ly/3yE82X3

Esta aplicación es otro screenmate de un pollo en pixel art escrito en Visual Basic.NET.

Los <b>desktop pets</b> o <b>Screenmates</b> fueron muy populares en la época de Windows95/98/XP pero debido a la inclusion de software malicioso por parte de terceros y la saturación de aplicaciones de este tipo, hicieron que al final la gente perdiera el interés y cayeran en desuso.

Esta aplicación es un pequeño tributo a los screenmates de antaño y a sus creadores.

# Funcionamiento interno

# Animaciones implementadas
- Caer al vacío
- Estrellarse contra el suelo
- Caminar sobre la barra de tareas
- Comer grano del suelo

# Sonidos
- De momento ninguno.

# APIs de Windows implementadas
- GetWindowRectangle() <-- Substituye a GetWindowRect() porque obtiene el tamaño de la ventana sin sombras en Win10.<br>
- DwmGetWindowAttribute()<br>
- GetWindow()<br>
- GetWindowText()<br>
- GetTopWindow()<br>
- IsWindowVisible()<br>
- EnumWindows()<br>
- EnumWindowsDelegate()<br>

# Fallos conocidos
- La colisión contra ventanas no es del todo correcta.
- Faltan depurar algunas animaciones.

# Agradecimientos
- <b>AdrianoTiger</b> por el código fuente de eSheep de donde he sacado algún recorte de código: https://github.com/Adrianotiger<br>
- <b>StackOverflow</b> por la ayuda y códigos fuente: https://stackoverflow.com<br>
