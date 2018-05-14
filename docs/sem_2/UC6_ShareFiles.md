# NeCo - Use-Case Specification: Share Files

## 1. Use-Case Name
Share Files

### 1.1 Brief Description
The app provides the user with the possibility to send a file to another user around you.

### 1.2 Screenshot Mockup

![mockup]


## 2. Flow of Events

### 2.1 Basic Flow

![basic flow]

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
(n/a)


<!-- Link definitions: -->
[basic flow]: https://github.com/Haus4/NeCo/raw/develop/docs/img/UC6_ShareFiles.jpg "Basic Flow: Receive Message"

[mockup]: https://github.com/Haus4/NeCo/raw/develop/docs/img/UC6_ShareFiles_Mockup.png "Chat Mockup"


