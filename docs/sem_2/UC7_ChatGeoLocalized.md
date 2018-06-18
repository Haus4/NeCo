# NeCo - Use-Case Specification: Chat Geo-Localized

## 1. Use-Case Name
Chat Geo-Localized

### 1.1 Brief Description
The app provides the user with the possibility to receive a message from another in a certain distance around you.

### 1.2 Screenshot Mockup

![mockup]


## 2. Flow of Events

### 2.1 Basic Flow

(n/a)

### 2.2 Alternative Flow
(n/a)


## 3. Special Requirements
### 3.1 Use chat protocol: TCP
The Android-App and the Xamarin-Backend should transfer their data with the Transmission Control Protocol (TCP). 

### 3.2 Geo-localization
The device running the app requires to have a GPS chip installed.


## 4. Preconditions

### 4.1 User key is generated
The chat messages are signed with a PGP-Key, which is generated upon opening the app.

### 4.2 User key is exchanged
The keys must have been exchanged before chating.

### 4.3 User is on the main page
The user should have already opened the NeCo Application on his smartphone and navigated to the main page.

### 4.4 GPS initialized
The global positioning system of the device is initialized and running. 
The app should have the right to request the users location.

## 5. Postconditions
(n/a)


## 6. Extension Points
(n/a)

## 7. Function Point calculation
|transaction|DET|RET|FTR|Complexity|
|---|---|---|---|---|
|external input|1|||low|
|external output|1|||low|
|external inquieries|0|||low|
|internal logical files|1||1|low|
|external interface files|1|||low|

This makes 27,3 FP

[Link to calculation website][fp calculation]

All function point calculation tables are also located in one spreadsheet. Please take a look at this [document][fpc spreadsheet].


<!-- Link definitions: -->
[fpc spreadsheet]:<https://github.com/Haus4/NeCo/raw/develop/docs/sem_2/time_estimation_uc.xlsx> "Function point calculation spreadsheet"

[fp calculation]: <http://groups.umd.umich.edu/cis/course.des/cis525/js/f00/harvey/FP_Calc.html#FPCalc> "FP calculation"

[mockup]: https://github.com/Haus4/NeCo/raw/develop/docs/img/UC1_2_3_Mockup.png "Chat Mockup"




