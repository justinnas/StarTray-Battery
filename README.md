<div align="center">
  <img src="https://github.com/user-attachments/assets/28666462-e956-4213-bc72-8ed6af153855" alt="StarTray Battery" height="200">
</div>



## StarTray Battery

**StarTray Battery** - lightweight and elegant open-source application that displays your laptop's battery percentage in the system tray.

**Supports**: Windows 10 and Windows 11 64-bit (x64) operating systems.

**Designed and developed by** [@justinnas](https://github.com/justinnas)

<br>

## Download
<div>
  <a href="https://github.com/justinnas/StarTray-Battery/releases/download/v1.2/StarTray-Battery-Setup.exe">
    <img src="https://github.com/user-attachments/assets/365b93d8-9934-4339-b5f4-d121ff574fe3" alt="Download button" height="80">
  </a>
</div>

<br>

You can download the latest version of StarTray Battery from [GitHub Releases](https://github.com/justinnas/StarTray-Battery/releases). Scroll down to 'Assets' section and choose between the installer or the portable version based on your preference.

<br>

***Disclaimer:** This appliaction requires administrative privileges in order to read battery data. Since this application is new, it might trigger some antivirus systems. If you prefer, you can review the source code and compile the application yourself.*

<br>

## Screenshots
***The icon can be customized through the themes menu.***
<div display="flex">
  <div>
<strong>White</strong> <br>
<img src="https://github.com/user-attachments/assets/4116479f-4c0f-4452-a25e-41cdaa74af50" width="50%">
  </div>
  <div>
<i>White (Charging)</i> <br>
<img src="https://github.com/user-attachments/assets/7a45c2c4-94bc-47cd-a43c-efbfb747c1a5" width="50%">
  </div>
    <br>
  <div>
    <strong>Dark</strong> <br>
<img src="https://github.com/user-attachments/assets/c571186e-5942-4446-aa8a-c65080bf91cd" width="50%">
    </div>
  <div>
    <i>Dark (Charging)</i> <br>
<img src="https://github.com/user-attachments/assets/b4dde101-e94f-475e-850c-396d65198cbb" width="50%">
    </div>
    <br>
  <div>
    <strong>Blue11</strong> <br>
<img src="https://github.com/user-attachments/assets/51e0cde0-b3f8-40c5-bb23-0dbaad6017fb" width="50%">
        </div>
  <div>
    <i>Blue11 (Charging)</i> <br>
<img src="https://github.com/user-attachments/assets/4dc4b72a-42f9-4645-a9d9-b70343023876" width="50%">
    </div>
</div>

<br>

The icons also work well with [StarTray Temperature](https://github.com/justinnas/StarTray-Temperature). If you need to monitor your computer's temperatures, check it out!

<br>

## How to use

This application is very simple to use. After launching the application, you will see battery icon in the system tray. <br>
You can right-click the icon to open the menu panel. Left-click opens up Windows battery settings window.

### Menu Panel

**Change Theme**

- **Change Icon Theme:**
    - Hover over the "Change Theme" tab.
    - Click on the desired theme to change the icon's appearance.

**Options**

- **Run on Startup:**
    - Hover over the "Options" tab.
    - Select "Run on Startup" to enable or disable the application’s ability to start when your system boots up.
- **Generate battery report:**
  <br>(*Generate battery report is a shortcut that uses Windows' powercfg tool to generate a detailed battery report*)
    - Hover over the "Options" tab.
    - Click on "Generate battery report". Battery report will open after generation and the file will be saved on desktop.

**Exit**

- **Close the Application:**
    - Click the "Exit" button to quit StarTray Battery.

<br>

## Source Code

If you prefer, you can review the source code and compile the application yourself. After compiling the application, copy the 'Resources' and 'Licenses' folders and their contents to the same directory as the compiled .exe file.

<br>

## License

This project uses the following libraries: System.ValueTuple (MIT License), Microsoft.Toolkit.Uwp.Notifications (MIT License), TaskScheduler (MIT License).

This project uses the Open Sans font, designed by Steve Matteson, licensed under SIL Open Font License, Version 1.1.

Their respective license files can be found [here](https://github.com/justinnas/StarTray-Battery/tree/main/Licenses).

StarTray Battery is licensed under GNU General Public License v3.0 license, please see the [license file](https://github.com/justinnas/StarTray-Battery/blob/main/LICENSE.txt) for more details.
