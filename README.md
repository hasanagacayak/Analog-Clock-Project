# Analog-Clock-Project
This project was developed during my internship at ASELSAN. It is a C# Windows Forms application that displays an analog clock based on the system time. Additionally, it shows the time remaining until the end of the working day and until mealtime, providing notifications when these times have passed.

# Overview
The Analog Clock Project is a practical application designed to visually represent the current system time using an analog clock interface. It also includes features to display and calculate the time remaining until significant daily events, such as the end of the working day and mealtimes. The application provides a user-friendly interface and real-time updates to keep users informed about their schedule.

# Features
Analog Clock Display: Visual representation of the current time using an analog clock interface.

Time Remaining Calculation: Real-time calculation and display of the time left until the end of the working day and until mealtime.

Notifications: Alerts the user if the working hours have ended or if mealtime has passed.

User Interface: Clear and intuitive interface with a main form (Form 1) that includes visual and textual time information.
# Technical Details
# Code Structure
Form1.cs: Contains the main logic for the analog clock and time calculations.

Form1.Designer.cs: Manages the design and layout of the main form.

Program.cs: The entry point of the application.

# Key Components
Analog Clock Design:

Two PictureBox controls are used to render the analog clock.

Clock hands (second, minute, and hour) are drawn using the DrawLine method.

Current time is retrieved using DateTime.Now, and positions of clock hands are adjusted accordingly.

Time Calculation:

A Timer control updates the clock and calculates the remaining time.

Functions msCoord and hrCoord calculate the angles of the clock hands based on 360-degree rotation logic.

# Conclusion

The Analog Clock Project demonstrates the integration of visual elements and real-time data processing in a C# Windows Forms application. It is a useful tool for tracking time and staying aware of important daily milestones. This project showcases practical application development skills, from user interface design to functional coding, and serves as a foundation for further enhancements.
