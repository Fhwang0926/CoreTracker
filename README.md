

# CoreTracker 

![GitHub release (latest by date)](https://img.shields.io/github/v/release/Fhwang0926/CoreTracker)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Status: ing](https://img.shields.io/badge/Status-dev_ing-red.svg)](https://github.com/Fhwang0926/CoreTracker)

[![Status: ing](https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/CoreTracker.png)](https://github.com/Fhwang0926/CoreTracker)

***
We can see by graphic about the status of Logical Processor

<div>
  <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/form.ico" width="75" style="display: inline-block:">
  <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/cpu.png" width="75" style="display: inline-block:">
  <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/ram.png" width="75" style="display: inline-block:">
  <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/gpu.png" width="75" style="display: inline-block:">
  <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/mainboard.png" width="75" style="display: inline-block:">
</div>


_ _ _

# Summary

- What is it ?
- How work it
- Demo
- Installation
- Support
- How bug report
- New Features!

___

## 1. What is it

___

### "Check the status of my computer with Trayicon."

> It's a time when most people learn coding and they use PCs a lot.
> Some developers consider performance.
> Some YouTubers test their benchmarks.
> Some people surf the web or create documents
> 
> Many benchmarks and resource checking programs are not intuitive.
> I need to pop up a new window, and I need to do more.
> 
> I hate it so much.
> 
> In developing as usual.
> As usual, on the Internet.
> Writing the document as usual.
> 
> My computer is
> 
> How busy are you?
> When are you busy?
> When is the load in my work?
> 
> I made it because I wanted to check.
> 
> Do you know when your computer is busy?
> 
> "As usual" is important.

## 2. how work it

> find data => status => display on trayicon

### Display criteria for tray icons

| Status | Color | Example |
| ------ | ------ | ------ |
| 0 <= status < 20 | GREEN | <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/Properties/status/10.ico" width="20"> |
| 20 <= status < 40 | YELLO | <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/Properties/status/20.ico" width="20"> |
| 40 <= status < 60 | ORANGE | <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/Properties/status/40.ico" width="20"> |
| 60 <= status < 80 | ORANGE | <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/Properties/status/60.ico" width="20"> |
| 80 <= status | RED  | <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/Properties/status/80.ico" width="20"> |

### Sample taskbar
![alt text](https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/taskbar.png "Logo Title Text 1")


Function Explain Start

| Name | Action |
| ------ | ------ |
| Auto Update | Auto-update check and notice, if you want to can continue update from recently version |
| Auto Start | Auto start when system turn on|
| CPU Temperature | Displays cpu temperature using trayicon|
| GPU(Graphic) Temperature | Displays GPU(Graphic temperature using trayicon|
| Mainboard Temperature | Displays Mainboard temperature  using trayicon|
| RAM Usage | Displays RAM Usage using trayicon|
| Show TrayIcon | Can enable or disable showing tray icon on the taskbar(is recommended setting)|
| Disabled Busy Alert | Display a notification when CPU utilization is 80% overall (if there is a continuous load, it will be displayed at regular intervals). |
| Refresh speed | trayicon refresh cycle setting : Fast(:=1s) or Normal(:=3s) or Slow(:=5s)|
| [Icon-Menu]Hide | The CoreTracker program Hide main windows |
| [Icon-Menu]Show | The CoreTracker program Show main windows |
| [Icon-Menu]Exit | The CoreTracker program close action |
| [Icon-Menu]Report | find out a bug, or want to a new function, can write on GitHub issue(login required) |
| [Icon-Menu]Update | Custom action for the update to new version|
| [Icon-Menu]Reset | Reset CPUs status watcher |


[CoreTarackerHelper] : excute tray icon refresh when uninstall using

Function Explain End
___

## DEMO with youtube (Attention Task bar)


[![CoreTracker](http://img.youtube.com/vi/rdZ1RNOGpvo/0.jpg)](http://www.youtube.com/watch?v=rdZ1RNOGpvo "CoreTracker")

___

## Installation 

1. dependency(support x86 and x64)

> <= dotnet framework 3.5

2. Many core(?)  :D :D :D
3. testing tool(if you want to) ~ is optional

### Set Trayicon area manual
> trayicon area setting on windows(can setting toggle on CoreTracker application)
#### 1. Click mouse right button on taskbar
<img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/set_trayicon_1.png" width="300">

#### 2. Click trayicon area setting
<img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/set_trayicon_2.png" width="300">

#### 3. Can set all show tray icon on the taskbar or selected application tray icon on the taskbar
<img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/set_trayicon_3.png" width="300">
___

## New Features

- Auto Bug Report
- memory reduce
- Add Korean language


[CoreTarackerHelper]: <https://github.com/Fhwang0926/CoreTrackerHelper>