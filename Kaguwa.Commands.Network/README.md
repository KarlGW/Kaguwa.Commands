#Kaguwa.Commands

A library containing PowerShell Cmdlets.

##About

This library will hold commands that utilises other libraries that I'll create to ease my own day to day work
with PowerShell and Microsoft Operating Systems.

As of this writing it is implemented in the following PowerShell module: [NetworkConnection](https://github.com/KarlGW/NetworkConnection)

##Build

Clone the repository to your system.

`git clone https://github.com/KarlGW/Kaguwa.Commands`

Open it in Visual Studio and build the solution.

Include it in a PowerShell module.


##Cmdlets
So far these Cmdlets (commands) have been implemented.

###GetNetworkConnectionCommand
This cmdlet uses the Kaguwa.Network library to query the operating system on the existing and available TCP/UDP connections (IPv4 and IPv6).
It's purpose is to mimic `netstat -ano` but instead return an object, and make it easier to filter the results with parameters.

##Versions and updates

###0.1.6233.37465
One change.

* Fixed `ProcessId` to not use any matching.

###0.1.6229.38278
First used version.