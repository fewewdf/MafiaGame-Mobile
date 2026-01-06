# MafiaGame-Mobile

A mobile implementation of the classic Mafia party game, bringing the excitement of social deduction gameplay to your smartphone.

## Project Description

MafiaGame-Mobile is a multiplayer party game application that brings the traditional Mafia/Werewolf game to mobile platforms. Players take on roles as either townspeople, mafia members, or special characters with unique abilities. The game combines strategy, deduction, and social interaction as players work to identify and eliminate the mafia before they take over the town.

### Features

- **Multiplayer Gameplay**: Play with friends and family in real-time
- **Role-based Mechanics**: Multiple character roles with unique abilities
- **Day/Night Cycles**: Dynamic gameplay with distinct day and night phases
- **Real-time Chat**: Communicate with other players during discussions
- **Game Statistics**: Track wins, losses, and player performance
- **Customizable Settings**: Adjust game rules and player count
- **Cross-platform Support**: Play on iOS and Android devices

## System Requirements

### Mobile Requirements
- **iOS**: Version 12.0 or later
- **Android**: Version 8.0 (API level 26) or later
- **RAM**: Minimum 2GB recommended
- **Storage**: Approximately 100MB of free space

### Development Requirements
- Xcode 12.0+ (for iOS development)
- Android Studio 4.0+ (for Android development)
- Node.js 14.0+ (if using JavaScript/React Native)
- Git for version control

## Setup Instructions

### Prerequisites

Before you begin, ensure you have the following installed:
- Git
- Your preferred IDE (Xcode for iOS, Android Studio for Android)
- Node.js and npm (if applicable)

### Installation Steps

1. **Clone the Repository**
   ```bash
   git clone https://github.com/fewewdf/MafiaGame-Mobile.git
   cd MafiaGame-Mobile
   ```

2. **Install Dependencies**
   ```bash
   npm install
   # or if using CocoaPods for iOS
   cd ios && pod install && cd ..
   ```

3. **Environment Setup**
   - Copy `.env.example` to `.env` (if applicable)
   - Update environment variables with your configuration

4. **Build and Run**

   **For iOS:**
   ```bash
   npx react-native run-ios
   # or open the .xcworkspace file in Xcode and run
   ```

   **For Android:**
   ```bash
   npx react-native run-android
   # Ensure an Android emulator is running or a device is connected
   ```

5. **Verify Installation**
   - Launch the app on your device/emulator
   - Check that all features load correctly

## Development Guide

### Project Structure
```
MafiaGame-Mobile/
├── ios/                 # iOS-specific code
├── android/             # Android-specific code
├── src/
│   ├── components/      # Reusable UI components
│   ├── screens/         # App screens
│   ├── services/        # API and game logic services
│   ├── utils/           # Utility functions
│   └── assets/          # Images, fonts, and other assets
├── package.json         # Dependencies and scripts
└── README.md           # This file
```

### Running Tests
```bash
npm test
```

### Building for Production

**iOS:**
```bash
cd ios && xcodebuild -workspace MafiaGame.xcworkspace -scheme MafiaGame -configuration Release
```

**Android:**
```bash
cd android && ./gradlew assembleRelease
```

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

Please ensure your code follows the project's code style and includes appropriate documentation.

## Troubleshooting

### Common Issues

**Issue: Dependencies not installing**
- Solution: Clear npm cache with `npm cache clean --force` and retry

**Issue: App crashes on startup**
- Solution: Ensure all native modules are correctly linked with `react-native link`

**Issue: Android build fails**
- Solution: Update Android SDK tools and check your Java version

For more help, check the [Issues](https://github.com/fewewdf/MafiaGame-Mobile/issues) page.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Contact

For questions or feedback, please reach out to the project maintainers or open an issue on GitHub.

---

**Last Updated**: 2026-01-06
