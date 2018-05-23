# NeCo - Use-Case Specification: Visualize Lobby

## 1. Use-Case Name
Visualize Lobby

### 1.1 Brief Description
The app provides the server with the possibility to visualisze lobbies.

### 1.2 Screenshot Mockup
(n/a)

## 2. Flow of Events

### 2.1 Basic Flow

<!-- ![basic flow] -->


### 2.2 Alternative Flow
(n/a)


## 3. Special Requirements
### 3.1 Use chat protocol: TCP
The Android-App and the Xamarin-Backend should transfer their data with the Transmission Control Protocol (TCP). 

### 3.1. Use encryption library: Signal Protocol
The client uses the encryption library to encrypt the message.


## 4. Preconditions

### 4.1 User key is generated
The chat messages are signed with a PGP-Key, which is generated upon opening the app.

### 4.2 User key is exchanged
The keys must have been exchanged before chating.

### 4.3 User is on the main page
The user should have already opened the NeCo Application on his smartphone and navigated to the main page.


## 5. Postconditions
(n/a)


## 6. Extension Points
(n/a)

## 7. Function Point calculation
|transaction|DET|RET|FTR|Complexity|
|---|---|---|---|---|
|external input|0|||low|
|external output|1|||low|
|external inquieries|1|||low|
|internal logical files|||3|low|
|external interface files|||1|low|

This makes 34,9 FP

[Link to calculation website][fp calculation]

All function point calculation tables are also located in one spreadsheet. Please take a look at this [document][fpc spreadsheet].


<!-- Link definitions: -->
[fpc spreadsheet]:<https://github.com/Haus4/NeCo/raw/develop/docs/sem_2/time_estimation_uc.xlsx> "Function point calculation spreadsheet"

[fp calculation]: <http://groups.umd.umich.edu/cis/course.des/cis525/js/f00/harvey/FP_Calc.html#FPCalc> "FP calculation"

[basic flow]: <link> "Relay Message Basic Flow"

