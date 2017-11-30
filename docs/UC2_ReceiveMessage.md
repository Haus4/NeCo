# NeCo - Use-Case Specification: Receive Message

## 1. Use-Case Name
Receive Message

### 1.1 Brief Description
The app provides the user with the possibility to receive a message from another around you.

### 1.2 Screenshot Mockup

![][mockup]


## 2. Flow of Events

### 2.1 Basic Flow

![][basic flow]


The `.feature`-file can be found [here][feature].

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
[basic flow]: https://github.com/Haus4/NeCo/tree/develop/docs/img/UC1_ReceiveMessage_UCD.jpg "Basic Flow: Receive Message"

[mockup]: https://github.com/Haus4/NeCo/raw/master/docs/img/UC1_2_3_Mockup.png "Chat Mockup"

[feature]: https://github.com/Haus4/NeCo/tree/develop/docs/UC2.feature "Feature description"


