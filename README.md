# What is this?
This package is a plug-in that adds a Midi loopback to Minis, allowing feedback to be sent to the Midi controller.

# How to Install
Be sure to install [Minis](https://github.com/keijiro/Minis) and InputSystem before installing the package.  
Then install it from the package manager.

# How to use
The following two scripts are required to loop back.
1. Singleton Midi Device Manager
2. MidiLoopOut  
Attach each of the two scripts to an arbitrary object.
MidiLoopOut" should be attached to the object where the PlayerInput component resides.

# MidiLoopOut Class
A common use of this package is to utilize the firing events of InputAction.
MidiLoopOut" provides two examples of "OnActionLoopBack" and "DebugLogAndLoopBack" that can be registered as events.

### Variables
- **Device Name**:The name of the device to loop back to. Partial match.  
- **Channel** : Specify the channel you wish to transmit(1~16).

Finally, to avoid collision of midi signals when multiple devices are connected, use the "Midi Device Assigner" included in Minis to limit the devices that can fire events.
Usually, the same device specified in "MidiLoopOut" can be used for loopback. It is also possible to specify a different device.  
![image](https://user-images.githubusercontent.com/39334911/175524947-ac794787-6809-4d63-9866-b8f7d13778b8.png)
