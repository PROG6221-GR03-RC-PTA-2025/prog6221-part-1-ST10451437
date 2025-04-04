using System;
using System.Media;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace CybersecurityAwarenessBot
{
    class Program
    {

        // Automatic properties for user information
        public static string UserName { get; set; }
        public static bool IsRunning { get; set; } = true;

        private static Dictionary<string, (string definition, string components, string causes, string effects, string example, string activity, string icon)> topicContent =
    new Dictionary<string, (string, string, string, string, string, string, string)>(StringComparer.OrdinalIgnoreCase)
{
    { "password", (
        "Strong passwords are crucial! They should be at least 12 characters long, include uppercase and lowercase letters, numbers, and special characters. Never reuse passwords across different accounts. Consider using a password manager!",

        "Components of Password Security:\n" +
        "1. Length: Longer passwords provide more security against brute force attacks.\n" +
        "2. Complexity: Mix of character types (uppercase, lowercase, numbers, symbols).\n" +
        "3. Uniqueness: Different passwords for different accounts.\n" +
        "4. Management: Secure storage using password managers.\n" +
        "5. Rotation: Periodic password changes for critical accounts.\n" +
        "6. MFA Integration: Passwords combined with other authentication factors.",

        "Common Causes of Password Vulnerabilities:\n" +
        "1. Password Reuse: Using the same password across multiple accounts.\n" +
        "2. Simple Passwords: Using common words, patterns, or personal information.\n" +
        "3. Writing Passwords Down: Physical exposure of credentials.\n" +
        "4. Sharing Passwords: Telling others your passwords or credentials.\n" +
        "5. Social Engineering: Falling for phishing or manipulation techniques.\n" +
        "6. Default Passwords: Not changing manufacturer or system default credentials.",

        "Effects of Poor Password Security:\n" +
        "1. Account Compromise: Unauthorized access to personal or work accounts.\n" +
        "2. Identity Theft: Criminals using your identity for fraudulent activities.\n" +
        "3. Financial Loss: Unauthorized transactions or purchases with your accounts.\n" +
        "4. Data Breaches: Compromised passwords leading to larger organizational breaches.\n" +
        "5. Credential Stuffing: Compromised passwords used to attempt access to other services.\n" +
        "6. Reputation Damage: Personal or organizational reputation harm from compromised accounts.",

        "Example: 'Password123' is weak, but 'T4k3!C@r3_0f-Y0ur$3lf' is strong because it mixes character types and is long enough to resist brute force attacks.",

        "Activity: Create a strong password for a fictional account following the guidelines. Then, check its strength using the NIST guidelines: 1) At least 12 characters 2) Mix of character types 3) No common dictionary words 4) Not based on personal information.",

        @"
    +=========+
    |  *****  |
    |  * * *  |
    |  *****  |
    +==+   +==+"
    )},

    { "phishing", (
        "Phishing attacks try to trick you into revealing sensitive information. Always verify the sender's email address, be suspicious of unexpected attachments, and never click on suspicious links. Legitimate organizations won't ask for your password via email.",

        "Components of Phishing Attacks:\n" +
        "1. Spoofed Sender: Fake email addresses mimicking legitimate organizations.\n" +
        "2. Social Engineering: Psychological manipulation using urgency, fear, or curiosity.\n" +
        "3. Malicious Links: URLs directing to fake websites or malware downloads.\n" +
        "4. Website Clones: Perfect copies of legitimate websites designed to steal credentials.\n" +
        "5. Data Collection Forms: Forms requesting sensitive personal or financial information.\n" +
        "6. Attack Vectors: Email, SMS (smishing), voice calls (vishing), or social media platforms.",

        "Common Causes of Phishing Vulnerability:\n" +
        "1. Lack of Awareness: Not knowing how to identify phishing attempts.\n" +
        "2. Urgency Response: Acting hastily when presented with urgent messages.\n" +
        "3. Trust Exploitation: Blindly trusting emails that appear to be from known entities.\n" +
        "4. Poor Email Security: Lack of proper email filtering and security tools.\n" +
        "5. Curiosity: Clicking on links or attachments out of curiosity without verification.\n" +
        "6. Mobile Device Viewing: Harder to spot phishing indicators on smaller screens.",

        "Effects of Successful Phishing Attacks:\n" +
        "1. Credential Theft: Stolen usernames, passwords, and other account credentials.\n" +
        "2. Financial Fraud: Unauthorized access to banking or payment systems.\n" +
        "3. Data Breaches: Unauthorized access to sensitive organizational data.\n" +
        "4. Malware Installation: Deployment of ransomware, spyware, or other malicious software.\n" +
        "5. Identity Theft: Use of stolen personal information for fraudulent purposes.\n" +
        "6. Business Email Compromise: Use of compromised email accounts for further attacks.",

        "Example: You receive an email claiming to be from your bank stating 'Your account has been compromised, click here to verify your information.' The link leads to a fake website designed to steal your credentials.",

        "Activity: Review these email subjects and identify which ones might be phishing attempts:\n1. 'Your Amazon order #12345 has shipped'\n2. 'URGENT: Your account access will be terminated'\n3. 'Netflix: Update your payment information'\n4. 'Meeting notes from yesterday'",

        @"
    .---------.
    |><)))'>   |
    '---------'"
    )},

    { "safe browsing", (
        "For safe browsing: keep your browser updated, use HTTPS websites, be careful what you download, avoid public Wi-Fi for sensitive transactions, and consider using a VPN for added security.",

        "Components of Safe Browsing:\n" +
        "1. Browser Security: Up-to-date browsers with security features enabled.\n" +
        "2. Connection Security: HTTPS and secure connections for all sensitive transactions.\n" +
        "3. URL Verification: Checking website addresses before interacting with them.\n" +
        "4. Download Caution: Being selective about what files and programs you download.\n" +
        "5. Cookie Management: Regular clearing of cookies and managing site permissions.\n" +
        "6. Privacy Tools: Using private browsing modes and tracking blockers when appropriate.",

        "Common Causes of Unsafe Browsing:\n" +
        "1. Outdated Software: Using browsers with known security vulnerabilities.\n" +
        "2. Ignoring Warnings: Bypassing browser security warnings about insecure sites.\n" +
        "3. Careless Downloading: Downloading files from untrusted sources.\n" +
        "4. Unsecured Networks: Using public Wi-Fi without additional protection.\n" +
        "5. Over-permissive Settings: Allowing websites excessive permissions to device features.\n" +
        "6. Clicking Recklessly: Interacting with pop-ups, ads, or unknown redirect links.",

        "Effects of Unsafe Browsing:\n" +
        "1. Malware Infection: Downloading and installing malicious software unknowingly.\n" +
        "2. Data Theft: Interception of sensitive information during transmission.\n" +
        "3. Credential Theft: Stolen passwords and login information.\n" +
        "4. Privacy Violations: Excessive tracking and profiling of online activities.\n" +
        "5. System Compromise: Full system access by malicious actors through browser exploits.\n" +
        "6. Financial Loss: Unauthorized transactions due to stolen payment information.",

        "Example: When shopping online, always check that the website URL begins with 'https://' and has a padlock icon in the address bar before entering payment information.",

        "Activity: Visit a favorite website and check: 1) Is it using HTTPS? 2) Can you find their privacy policy? 3) Does the website ask for unnecessary personal information? 4) Are there any suspicious pop-ups or redirects?",

        @"
    .-----------. 
    |  .-----.  |
    |  | [P] |  |
    |  '-----'  |
    '-----------'"
    )},

    { "malware", (
        "Malware is malicious software designed to harm your system. Protect yourself by keeping your OS and software updated, using antivirus, avoiding suspicious downloads, and backing up your data regularly.",

        "Components of Malware:\n" +
        "1. Viruses: Self-replicating code that attaches to legitimate programs.\n" +
        "2. Trojans: Malicious software disguised as legitimate applications.\n" +
        "3. Spyware: Software that monitors user activity without consent.\n" +
        "4. Adware: Software that displays unwanted advertisements.\n" +
        "5. Ransomware: Encrypts data and demands payment for decryption keys.\n" +
        "6. Rootkits: Tools that enable unauthorized access while hiding their presence.\n" +
        "7. Worms: Self-replicating malware that spreads across networks.",

        "Common Causes of Malware Infection:\n" +
        "1. Suspicious Downloads: Installing software from untrusted sources.\n" +
        "2. Email Attachments: Opening malicious files received via email.\n" +
        "3. Drive-by Downloads: Visiting compromised websites that silently install malware.\n" +
        "4. Removable Media: Using infected USB drives or external storage devices.\n" +
        "5. Outdated Software: Unpatched vulnerabilities in operating systems and applications.\n" +
        "6. Social Engineering: Being tricked into installing malware through manipulation.\n" +
        "7. Weak Passwords: Compromised accounts leading to system access.",

        "Effects of Malware Infection:\n" +
        "1. System Slowdown: Degraded performance and responsiveness.\n" +
        "2. Data Loss: Deletion, corruption, or encryption of important files.\n" +
        "3. Information Theft: Stealing personal data, passwords, and financial information.\n" +
        "4. Privacy Violations: Monitoring of user activities and communications.\n" +
        "5. Resource Hijacking: Unauthorized use of system resources for cryptomining or botnets.\n" +
        "6. Financial Loss: Direct theft or ransom payments.\n" +
        "7. System Damage: Permanent harm to software or hardware components.",

        "Example: A free game download from an unofficial site might contain a trojan that secretly installs a keylogger to capture your passwords and financial information.",

        "Activity: Create a personal malware prevention checklist including: 1) Software that needs regular updates 2) Backup schedule for important files 3) Safe download sources for programs you commonly use 4) Warning signs of potential infection",

        @"
    +----------+
    |  /\__/\  |
    |  (XX)  |
    |  //\\  |
    +----------+"
    )},

    { "2fa", (
        "Two-Factor Authentication (2FA) adds an extra layer of security by requiring something you know (password) and something you have (like your phone). Enable it whenever possible on your accounts!",

        "Components of 2FA:\n" +
        "1. Knowledge Factor: Something you know (password, PIN, pattern).\n" +
        "2. Possession Factor: Something you have (smartphone, security key, smart card).\n" +
        "3. Inherence Factor: Something you are (fingerprint, face, voice recognition).\n" +
        "4. Authentication Apps: Software generating time-based one-time passwords (TOTP).\n" +
        "5. SMS/Email Codes: One-time codes sent to registered devices or addresses.\n" +
        "6. Hardware Tokens: Physical devices generating security codes or using cryptographic authentication.",

        "Common Causes of 2FA Vulnerabilities:\n" +
        "1. SMS Interception: SIM swapping attacks or telecommunications vulnerabilities.\n" +
        "2. Phishing: Advanced attacks that capture and replay 2FA codes in real-time.\n" +
        "3. Social Engineering: Manipulating users to share their 2FA codes.\n" +
        "4. Recovery Methods: Weak account recovery processes that bypass 2FA.\n" +
        "5. Malware: Specialized software that can intercept 2FA codes from devices.\n" +
        "6. User Resistance: Avoiding 2FA due to perceived inconvenience.",

        "Effects of Using or Not Using 2FA:\n" +
        "1. Enhanced Security: Significant reduction in successful account compromise attempts.\n" +
        "2. Credential Protection: Passwords alone are insufficient for account access.\n" +
        "3. Early Warning: Notifications of 2FA requests can alert users to unauthorized access attempts.\n" +
        "4. Business Protection: Reduced risk of data breaches and unauthorized system access.\n" +
        "5. Compliance: Meeting regulatory requirements for access security.\n" +
        "6. Recovery Challenges: Potential loss of access if 2FA devices are lost without backup methods.",

        "Example: After entering your password on a website, you receive a text message with a 6-digit code that you must also enter to complete the login process.",

        "Activity: Make a list of your 5 most important online accounts and check which ones support 2FA. For those that do, plan to enable it within the next week. For those that don't, consider if there are alternative services that offer better security.",

        @"
    .-----. .-----.
    | ### | | 123 |
    | ### | | 456 |
    | ### | | 789 |
    '-----' '-----'"
    )},

    { "vpn", (
        "A Virtual Private Network (VPN) encrypts your internet connection, protecting your data from prying eyes. It's essential when using public Wi-Fi and helps maintain privacy online.",

        "Components of VPN Security:\n" +
        "1. Encryption: Protocols and algorithms that secure data transmission.\n" +
        "2. Tunneling: Creation of secure pathways for data through insecure networks.\n" +
        "3. Server Network: Distributed servers allowing connection through different locations.\n" +
        "4. Authentication: Methods to verify user identity before establishing connections.\n" +
        "5. IP Masking: Hiding the user's real IP address and location.\n" +
        "6. Kill Switch: Feature that cuts internet connection if VPN connection drops.\n" +
        "7. Split Tunneling: Selective routing of traffic through VPN or direct connection.",

        "Common VPN Vulnerabilities and Considerations:\n" +
        "1. Logging Practices: Some VPNs keep user activity logs despite privacy claims.\n" +
        "2. DNS Leaks: Unprotected DNS queries revealing browsing activities.\n" +
        "3. WebRTC Leaks: Browser API features that can reveal real IP addresses.\n" +
        "4. Jurisdiction: Legal requirements for data retention in the VPN provider's country.\n" +
        "5. Free VPN Risks: No-cost services often monetize through data collection and ads.\n" +
        "6. Misconfiguration: Improperly set up VPNs may not provide full protection.",

        "Effects of Using VPNs:\n" +
        "1. Enhanced Privacy: Protection from ISP monitoring and tracking.\n" +
        "2. Public Wi-Fi Security: Encrypted connection on otherwise insecure networks.\n" +
        "3. Geo-restriction Bypass: Access to content restricted by geographic location.\n" +
        "4. Censorship Circumvention: Access to websites and services blocked in certain regions.\n" +
        "5. Reduced Targeted Advertising: Limited ability for sites to track and profile users.\n" +
        "6. Potential Speed Reduction: Additional encryption and routing may slow connections.\n" +
        "7. False Sense of Security: Users may overestimate the protection level provided.",

        "Example: When using coffee shop Wi-Fi, a VPN creates an encrypted tunnel for your data, preventing others on the same network from seeing your online activities or intercepting your information.",

        "Activity: Research 3 reputable VPN services and compare their features: 1) Privacy policy 2) Connection speed 3) Server locations 4) Price 5) Device compatibility",

        @"
    .-----------. 
    | # -> # -> |
    | v # v #   |
    | [=======] |
    '-----------'"
    )},

    { "ransomware", (
        "Ransomware encrypts your files and demands payment for the decryption key. Prevent it by keeping software updated, backing up data regularly, and avoiding suspicious downloads.",

        "Components of Ransomware Attacks:\n" +
        "1. Infection Vector: Methods used to deliver ransomware (phishing, exploits, etc.).\n" +
        "2. Encryption Engine: Algorithms used to lock files and make them inaccessible.\n" +
        "3. Command & Control: Remote servers controlling the ransomware operation.\n" +
        "4. Payment System: Usually cryptocurrency wallets for anonymous ransom collection.\n" +
        "5. Countdown Timer: Time limit for payment before price increases or data is destroyed.\n" +
        "6. Data Exfiltration: Modern ransomware often steals data before encryption (double extortion).\n" +
        "7. Persistence Mechanisms: Methods to maintain access even after system reboots.",

        "Common Causes of Ransomware Infection:\n" +
        "1. Phishing Emails: Opening malicious attachments or links in emails.\n" +
        "2. RDP Exploitation: Weak Remote Desktop Protocol credentials or unpatched vulnerabilities.\n" +
        "3. Software Vulnerabilities: Unpatched operating systems and applications.\n" +
        "4. Drive-by Downloads: Visiting compromised or malicious websites.\n" +
        "5. Supply Chain Attacks: Compromised software updates or third-party services.\n" +
        "6. Social Engineering: Being manipulated into installing malicious software.\n" +
        "7. Malvertising: Malicious code embedded in online advertisements.",

        "Effects of Ransomware Attacks:\n" +
        "1. Data Encryption: Inaccessible files and systems until decryption.\n" +
        "2. Operational Disruption: Business processes and services halted.\n" +
        "3. Financial Loss: Direct costs (ransom) and indirect costs (downtime, recovery).\n" +
        "4. Reputational Damage: Loss of customer trust and market standing.\n" +
        "5. Data Breach: Exposure of sensitive information stolen before encryption.\n" +
        "6. Recovery Challenges: Even with backups, full restoration can be complex and time-consuming.\n" +
        "7. Legal Consequences: Potential regulatory penalties for inadequate security measures.",

        "Example: After opening an attachment from an unknown email, your computer screen displays a message demanding $500 in Bitcoin to unlock your now-encrypted files.",

        "Activity: Create a ransomware response plan: 1) Which files would you prioritize backing up? 2) How often should they be backed up? 3) Where would you store backups (cloud/physical)? 4) What steps would you take if infected?",

        @"
    +----------+
    | .-. .-.  |
    | |$| |$|  |
    | '-' '-'  |
    +----------+"
    )},

    { "social engineering", (
        "Social engineering uses psychological manipulation to trick people into revealing sensitive information or performing actions that compromise security. It exploits human trust and natural tendencies rather than technical vulnerabilities.",

        "Components of Social Engineering:\n" +
        "1. Pretexting: Creating a fabricated scenario to extract information.\n" +
        "2. Baiting: Offering something enticing to entrap the victim.\n" +
        "3. Quid Pro Quo: Offering a service or benefit in exchange for information.\n" +
        "4. Tailgating: Following someone into a restricted area without authorization.\n" +
        "5. Impersonation: Posing as a trusted individual or authority figure.\n" +
        "6. Scareware: Convincing users their system is infected to make them install malware.\n" +
        "7. Manipulation: Exploiting emotions like fear, greed, or curiosity.",

        "Common Causes of Social Engineering Vulnerability:\n" +
        "1. Lack of Awareness: Not recognizing common social engineering tactics.\n" +
        "2. Desire to Help: Natural inclination to assist others.\n" +
        "3. Trust in Authority: Tendency to comply with requests from apparent authority figures.\n" +
        "4. Fear and Urgency: Making hasty decisions when pressured.\n" +
        "5. Curiosity: Tendency to investigate unusual or interesting situations.\n" +
        "6. Complacency: Becoming careless about security protocols over time.\n" +
        "7. Insufficient Training: Lack of regular security awareness education.",

        "Effects of Successful Social Engineering:\n" +
        "1. Data Breaches: Unauthorized access to sensitive information.\n" +
        "2. Financial Loss: Direct theft or fraudulent transactions.\n" +
        "3. Identity Theft: Use of personal information for illicit purposes.\n" +
        "4. Network Compromise: Gaining access to protected systems and networks.\n" +
        "5. Reputation Damage: Loss of trust from customers, partners, or employers.\n" +
        "6. Operational Disruption: Business processes interrupted by security incidents.\n" +
        "7. Psychological Impact: Feeling of violation, embarrassment, or guilt after being manipulated.",

        "Example: A caller claiming to be from IT support asks for your login credentials to 'resolve a critical server issue,' creating a sense of urgency to bypass your normal security instincts.",

        "Activity: Role-play different social engineering scenarios with colleagues or friends. Take turns being the attacker and the target, then discuss how you identified (or missed) the manipulation techniques being used.",

        @"
    .----------.
    |  __/\__  |
    | /(o)(o)\ |
    | \  ()  / |
    '----------'"
    )},

    { "iot security", (
        "Internet of Things (IoT) security focuses on protecting connected devices and networks in the growing ecosystem of smart devices. These devices often have limited security features but can provide access to sensitive systems and data.",

        "Components of IoT Security:\n" +
        "1. Device Authentication: Verifying the identity of connected devices.\n" +
        "2. Data Encryption: Protecting information transmitted between devices.\n" +
        "3. Network Security: Segmentation and protection of IoT device networks.\n" +
        "4. Firmware Updates: Regular security patches for device operating systems.\n" +
        "5. Access Controls: Managing who can access and control devices.\n" +
        "6. Physical Security: Protecting devices from unauthorized physical access.\n" +
        "7. Privacy Protections: Safeguarding data collected by IoT devices.",

        "Common IoT Security Vulnerabilities:\n" +
        "1. Default Credentials: Unchanged factory usernames and passwords.\n" +
        "2. Lack of Updates: Devices without security patches or update mechanisms.\n" +
        "3. Insecure Communications: Unencrypted data transmission between devices.\n" +
        "4. Poor Authentication: Weak or non-existent device authentication methods.\n" +
        "5. Privacy Concerns: Excessive data collection without user awareness.\n" +
        "6. Weak API Security: Vulnerable interfaces for device management.\n" +
        "7. Cross-Device Dependencies: Security chains only as strong as the weakest link.",

        "Effects of IoT Security Breaches:\n" +
        "1. Privacy Violations: Unauthorized access to personal spaces and information.\n" +
        "2. Network Compromise: Using IoT devices as entry points to larger networks.\n" +
        "3. Physical Safety Risks: Compromised devices affecting real-world safety.\n" +
        "4. DDoS Participation: Hijacked devices used in distributed denial of service attacks.\n" +
        "5. Data Theft: Collection of sensitive information from device sensors.\n" +
        "6. Resource Hijacking: Unauthorized use of device processing power or bandwidth.\n" +
        "7. Surveillance Capabilities: Compromised cameras or microphones used for spying.",

        "Example: An unsecured smart thermostat could allow attackers to know when a home is empty based on temperature settings, or could even serve as an entry point to the home's entire network.",

        "Activity: Create an inventory of all IoT devices in your home or office. Check if each device: 1) Has a changed password from default 2) Is receiving regular updates 3) Is on a separate network from your main computing devices 4) Has unnecessary features disabled",

        @"
    .----------.
    | [][][][][] |
    | [][--][][] |
    | [][][][][] |
    '----------'"
    )}
};
        private static Dictionary<string, string> responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
{
    // Existing responses
    { "hello", "Hello there, {0}! What cybersecurity topic would you like to discuss?" },
    { "hi", "Hi {0}! What cybersecurity topic would you like to discuss?" },
    { "how are you", "I'm functioning perfectly, {0}! What cybersecurity topic would you like to discuss?" },
    { "what's your purpose", "I'm designed to help raise awareness about cybersecurity issues and provide helpful tips to keep you safe online. What cybersecurity topic would you like to discuss?" },
    { "what can i ask you about", "You can ask me about password safety, phishing attacks, safe browsing habits, and other cybersecurity topics. What would you like to discuss?" },
    { "help", "I can provide information on various cybersecurity topics. You can ask about 'password safety', 'phishing', 'safe browsing', '2FA', 'malware', 'vpn', or 'ransomware'. What topic interests you?" },
    { "bye", "Goodbye, {0}! Stay safe online!" },
    { "exit", "Exiting the Cybersecurity Awareness Bot. Remember to stay vigilant online, {0}!" },
    
    // Added responses for greetings and small talk
    { "good morning", "Good morning, {0}! Ready to learn about cybersecurity today?" },
    { "good afternoon", "Good afternoon, {0}! What cybersecurity topic shall we explore?" },
    { "good evening", "Good evening, {0}! Let's enhance your cybersecurity knowledge." },
    { "thanks", "You're welcome, {0}! Cybersecurity education is my primary function." },
    { "thank you", "You're welcome, {0}! Cybersecurity awareness is crucial in the digital age." },
    
    // Added responses for bot-related questions
    { "who made you", "I was developed as a cybersecurity awareness tool. My purpose is to help users like you stay safe online." },
    { "what are you", "I am CYBERTITAN, an interactive educational bot designed to improve cybersecurity awareness through engaging conversations." },
    { "your name", "I am CYBERTITAN, your Cybersecurity Awareness Bot. How can I assist with your cybersecurity knowledge today?" },
    
    // Added responses for specific cybersecurity questions not covered by topics
    { "best antivirus", "The best antivirus is one that's regularly updated and suits your specific needs. Popular options include Windows Defender, Norton, Bitdefender, and Kaspersky. What specific cybersecurity topic would you like to explore?" },
    { "how to stay safe online", "Staying safe online involves using strong passwords, enabling 2FA, being cautious of phishing attempts, keeping software updated, using secure connections, and backing up your data regularly. Would you like to discuss any of these topics in more detail?" },
    { "what is cybersecurity", "Cybersecurity refers to the practice of protecting systems, networks, and programs from digital attacks. These attacks typically aim to access, change, or destroy sensitive information, extort money, or interrupt normal business processes. Which aspect of cybersecurity would you like to learn more about?" },
    
    // Added responses for confused users
    { "i don't know", "That's okay, {0}! Here are some popular topics you might want to explore: password safety, phishing, safe browsing, 2FA, malware, VPN, or ransomware. Which one interests you?" },
    { "not sure", "No problem, {0}. You could start with basics like 'password' security or 'phishing' awareness. What sounds most relevant to your daily online activities?" },
    
    // Added responses for topic requests that don't match exactly
    { "passwords", "Let me tell you about password security! Type 'password' to learn more." },
    { "password security", "Let me tell you about password security! Type 'password' to learn more." },
    { "password safety", "Let me tell you about password security! Type 'password' to learn more." },
    { "phishing attacks", "Let me tell you about phishing! Type 'phishing' to learn more." },
    { "safe browsing habits", "Let me tell you about safe browsing! Type 'safe browsing' to learn more." },
    { "two factor authentication", "Let me tell you about two-factor authentication! Type '2fa' to learn more." },
    { "two-factor authentication", "Let me tell you about two-factor authentication! Type '2fa' to learn more." },
    { "malware protection", "Let me tell you about malware! Type 'malware' to learn more." },
    { "vpn security", "Let me tell you about VPN security! Type 'vpn' to learn more." },
    { "virtual private network", "Let me tell you about VPN security! Type 'vpn' to learn more." },
    { "ransomware protection", "Let me tell you about ransomware! Type 'ransomware' to learn more." },
    { "social engineering attacks", "Let me tell you about social engineering! Type 'social engineering' to learn more." },
    { "iot security concerns", "Let me tell you about IoT security! Type 'iot security' to learn more." }
};

        static void Main(string[] args)
        {
            // Set console properties
            Console.Title = "◢◤ CYBERSECURITY AWARENESS BOT ◥◣";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            // Display the animated background - ADD THIS LINE
            DrawNeonBackground();

            // Play voice greeting`
            PlayVoiceGreeting();

            // Display Future Tech style ASCII logo
            DisplayFutureTechLogo();

            // Display loading animation
            DisplayLoadingAnimation();

            // Display welcome message with typing effect
            TypeWriteEffect("\n⚡ Welcome to the Cybersecurity Awareness Chatbot, known as CYBERTITAN ⚡", ConsoleColor.Cyan);
            Console.WriteLine();

            // Ask for user's name
            GetUserName();

            // Ask what topic they want to discuss after getting their name
            AskForTopic();

            // Main chat loop
            ChatLoop();

        }

        static void DrawNeonBackground()
        {
            // Save the current console properties
            int origWidth = Console.WindowWidth;
            int origHeight = Console.WindowHeight;
            ConsoleColor defaultForeground = Console.ForegroundColor;
            ConsoleColor defaultBackground = Console.BackgroundColor;

            // Set console properties for the animation
            Console.CursorVisible = false;

            // Characters for the matrix-like digital rain effect
            char[] neonChars = { '0', '1', '@', '#', '$', '%', '&', '*', '!', '+', '=', '?', ':', ';' };

            // Colors for the neon effect
            ConsoleColor[] neonColors = {
        ConsoleColor.Cyan,
        ConsoleColor.Magenta,
        ConsoleColor.DarkCyan,
        ConsoleColor.DarkMagenta,
        ConsoleColor.Blue,
        ConsoleColor.DarkBlue
    };

            // Create arrays to track digital rain properties
            int[] startPositions = new int[origWidth];
            int[] lengths = new int[origWidth];
            int[] speeds = new int[origWidth];
            int[] colorIndices = new int[origWidth];

            // Initialize the digital rain properties
            Random random = new Random();
            for (int i = 0; i < startPositions.Length; i++)
            {
                startPositions[i] = random.Next(-origHeight, 0);
                lengths[i] = random.Next(3, 10);
                speeds[i] = random.Next(1, 3);
                colorIndices[i] = random.Next(0, neonColors.Length);
            }

            // Draw the background for 3 seconds
            DateTime endTime = DateTime.Now.AddSeconds(3);
            while (DateTime.Now < endTime)
            {
                // Clear for redrawing
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();

                // Draw neon grid lines
                if (random.Next(0, 10) < 3) // Occasionally show grid
                {
                    // Draw horizontal grid lines
                    for (int y = 0; y < origHeight; y += 5)
                    {
                        Console.ForegroundColor = neonColors[random.Next(0, neonColors.Length)];
                        for (int x = 0; x < origWidth; x++)
                        {
                            if (x % 3 == 0) // Make grid dotted
                            {
                                Console.SetCursorPosition(x, y);
                                Console.Write('·');
                            }
                        }
                    }

                    // Draw vertical grid lines
                    for (int x = 0; x < origWidth; x += 10)
                    {
                        Console.ForegroundColor = neonColors[random.Next(0, neonColors.Length)];
                        for (int y = 0; y < origHeight; y++)
                        {
                            if (y % 2 == 0) // Make grid dotted
                            {
                                Console.SetCursorPosition(x, y);
                                Console.Write('|');
                            }
                        }
                    }
                }

                // Draw digital rain
                for (int i = 0; i < startPositions.Length; i++)
                {
                    // Only draw some columns for sparse effect
                    if (i % 3 != 0) continue;

                    // Update the rain position
                    startPositions[i] += speeds[i];

                    // Reset if the rain has moved off screen
                    if (startPositions[i] - lengths[i] > origHeight)
                    {
                        startPositions[i] = random.Next(-origHeight, 0);
                        lengths[i] = random.Next(3, 10);
                        speeds[i] = random.Next(1, 3);
                        colorIndices[i] = random.Next(0, neonColors.Length);
                    }

                    // Draw the digital rain characters
                    Console.ForegroundColor = neonColors[colorIndices[i]];
                    for (int j = 0; j < lengths[i]; j++)
                    {
                        int y = startPositions[i] - j;
                        if (y >= 0 && y < origHeight)
                        {
                            Console.SetCursorPosition(i, y);
                            Console.Write(neonChars[random.Next(0, neonChars.Length)]);
                        }
                    }
                }

                // Draw cybersecurity symbols randomly
                if (random.Next(0, 10) < 2) // Occasionally show symbols
                {
                    int symbolX = random.Next(0, origWidth - 10);
                    int symbolY = random.Next(0, origHeight - 5);
                    ConsoleColor symbolColor = neonColors[random.Next(0, neonColors.Length)];

                    string[] symbols = {
                @"  .---.   
 /   /    
[___]     ",
                @"  _____  
 |     | 
 |_____| ",
                @"  .===.  
 ///// 
 \\\\\  ",
                @"  _|_|_  
 |_____| 
 |_____| "
            };

                    string symbol = symbols[random.Next(0, symbols.Length)];
                    string[] lines = symbol.Split('\n');

                    Console.ForegroundColor = symbolColor;
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (symbolY + i < origHeight)
                        {
                            Console.SetCursorPosition(symbolX, symbolY + i);
                            Console.Write(lines[i]);
                        }
                    }
                }

                // Add glowing edges
                Console.ForegroundColor = neonColors[random.Next(0, neonColors.Length)];
                for (int i = 0; i < origWidth; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write('▄');
                    Console.SetCursorPosition(i, origHeight - 1);
                    Console.Write('▀');
                }

                for (int i = 1; i < origHeight - 1; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write('█');
                    Console.SetCursorPosition(origWidth - 1, i);
                    Console.Write('█');
                }

                Thread.Sleep(100);
            }

            // Restore console properties
            Console.CursorVisible = true;
            Console.ForegroundColor = defaultForeground;
            Console.BackgroundColor = defaultBackground;
            Console.Clear();
        }

        static void PlayVoiceGreeting()
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer(@"C:\Users\RC_Student_lab\source\repos\MyFirstProject\CyberSecurityAwarenessBot_2\bin\Debug\Greeting_Voice (online-audio-converter.com).wav"))
                {
                    player.Load();
                    player.PlaySync();
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] Voice module initialization failed: " + e.Message);
                Console.ResetColor();
            }
        }


        static void DisplayFutureTechLogo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
╔══════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                      ║");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
║   ██████╗██╗   ██╗██████╗ ███████╗██████╗ ████████╗██╗████████╗ █████╗ ███╗   ██╗  ║
║  ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗╚══██╔══╝██║╚══██╔══╝██╔══██╗████╗  ██║  ║
║  ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝   ██║   ██║   ██║   ███████║██╔██╗ ██║  ║
║  ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗   ██║   ██║   ██║   ██╔══██║██║╚██╗██║  ║
║  ╚██████╗   ██║   ██████╔╝███████╗██║  ██║   ██║   ██║   ██║   ██║  ██║██║ ╚████║  ║
║   ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚═╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═══╝  ║");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(@"
║                                                                                      ║
║               🔒 VIGILANCE NEVER SLEEPS 🔒                                         ║
║                                                                                      ║
║             [CYBER SECURITY INTELLIGENCE v1.0]                                       ║
╚══════════════════════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
        }
        static void DisplayLoadingAnimation()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("\nInitializing system");

            // Loading strip animation
            string[] frames = { "⠋", "⠙", "⠹", "⠸", "⠼", "⠴", "⠦", "⠧", "⠇", "⠏" };
            for (int i = 0; i < 20; i++)
            {
                Console.Write($" {frames[i % frames.Length]}");
                Thread.Sleep(100);
                Console.Write("\b\b");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" [SYSTEM ONLINE]");
            Console.ResetColor();

            // Progress bar with neon color effect
            Console.Write("\n[");
            for (int i = 0; i < 30; i++)
            {
                // Alternating between cyan and magenta for neon effect
                Console.ForegroundColor = i % 2 == 0 ? ConsoleColor.Cyan : ConsoleColor.Magenta;
                Console.Write("█");
                Thread.Sleep(30);
            }
            Console.ResetColor();
            Console.WriteLine("] 100%");

            Thread.Sleep(500);
        }

        static void GetUserName()
        {
            bool validName = false;

            while (!validName)
            {
                // Decorative border for user prompt
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
                Console.WriteLine("║            USER IDENTIFICATION REQUIRED                ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════╝");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n→ Enter your name: ");
                Console.ResetColor();

                UserName = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(UserName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] Identity verification failed. Retry required.");
                    Console.ResetColor();
                }
                else
                {
                    validName = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriteEffect($"\n>>> Identity confirmed: {UserName} <<<", ConsoleColor.Green);
                    TypeWriteEffect(">>> Cybersecurity protocols activated <<<", ConsoleColor.Green);
                }
            }
        }

        static void AskForTopic()
        {
            // Decorative border for topic selection
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              CHOOSE A CYBERSECURITY TOPIC               ║");
            Console.WriteLine("║                                                         ║");
            Console.WriteLine("║  • password   • phishing   • safe browsing   • 2fa      ║");
            Console.WriteLine("║  • malware    • vpn        • ransomware                 ║");
            Console.WriteLine("║                                                         ║");
            Console.WriteLine("║  Type 'help' for command list                           ║");
            Console.WriteLine("║  Type 'exit' to terminate session                       ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            TypeWriteEffect($"\n[CYBERTITAN v2.077] → Hello {UserName}! What cybersecurity topic would you like to discuss?", ConsoleColor.Green);
            Console.ResetColor();
        }

        // THIS IS THE MISSING METHOD THAT WAS CAUSING THE ISSUE
        static void TypeWriteEffect(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(10); // Adjust speed as needed
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        static void ProcessInput(string input)
        {
            // Check if the input is an exit command
            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                input.Equals("bye", StringComparison.OrdinalIgnoreCase))
            {
                string response = responses.ContainsKey(input) ?
                    string.Format(responses[input], UserName) :
                    $"Goodbye, {UserName}! Stay safe online!";

                TypeWriteEffect($"\n[CYBER-SEC v2.077] → {response}", ConsoleColor.Green);
                IsRunning = false;
                return;
            }

            // Check if the input is a basic command
            if (responses.ContainsKey(input))
            {
                TypeWriteEffect($"\n[CYBER-SEC v2.077] → {string.Format(responses[input], UserName)}", ConsoleColor.Green);
                return;
            }

            // Check if the input is a cybersecurity topic
            if (topicContent.ContainsKey(input))
            {
                DisplayTopicContent(input);
                return;
            }

            // If we get here, the input wasn't recognized
            TypeWriteEffect($"\n[CYBER-SEC v2.077] → I'm not sure what you mean by '{input}'. Type 'help' for a list of topics and commands.", ConsoleColor.Green);
        }


        static void DisplayTopicContent(string topic)
        {
            var content = topicContent[topic];

            // Display topic header with enhanced styling
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n╔══════════════════ {topic.ToUpper()} ══════════════════╗");
            Console.ResetColor();

            // Display definition
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n📘 Definition:");
            Console.ResetColor();
            TypeWriteEffect($"{content.definition}", ConsoleColor.Yellow);

            // Display components
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n🔧 Components:");
            Console.ResetColor();
            TypeWriteEffect($"{content.components}", ConsoleColor.DarkCyan);

            // Display causes
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n⚠️ Causes:");
            Console.ResetColor();
            TypeWriteEffect($"{content.causes}", ConsoleColor.DarkYellow);

            // Display effects
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n💥 Effects:");
            Console.ResetColor();
            TypeWriteEffect($"{content.effects}", ConsoleColor.Red);

            // Display example
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n🔍 {topic} Example:");
            Console.ResetColor();
            TypeWriteEffect($"{content.example}", ConsoleColor.Magenta);

            // Display activity
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n⚡ Practice Activity:");
            Console.ResetColor();
            TypeWriteEffect($"{content.activity}", ConsoleColor.Green);

            // Add a prompt for the next topic
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            TypeWriteEffect("\n[CYBER-SEC v2.077] → What other cybersecurity topic would you like to explore?", ConsoleColor.Green);
        }
        static void ChatLoop()
        {
            while (IsRunning)
            {
                // Display prompt for user input
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("\n➤ ");
                Console.ForegroundColor = ConsoleColor.White;

                // Get user input
                string input = Console.ReadLine().Trim();
                Console.ResetColor();

                // Process the input
                if (!string.IsNullOrWhiteSpace(input))
                {
                    ProcessInput(input);
                }
            }

            // Display exit message with animated goodbye
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                 TERMINATING SESSION                     ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            // Animated goodbye
            string goodbye = "[SYSTEM SHUTTING DOWN - REMEMBER TO STAY CYBER-SAFE]";
            TypeWriteEffect(goodbye, ConsoleColor.Red);

            // Countdown animation
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 3; i > 0; i--)
            {
                Console.Write($"\rExiting in {i}...");
                Thread.Sleep(1000);
            }
            Console.WriteLine("\rGoodbye!            ");
            Console.ResetColor();

            // Call the neon exit animation
            DrawNeonOutrance();

            // Program will exit naturally as the Main method concludes
        }

        private static void DrawNeonOutrance()
        {
            Console.Clear();
            Console.CursorVisible = false;

            // Draw digital rain effect first
            DrawDigitalRain();

            // Then show final exit message
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;

            string[] exitFrame = {
            "╔═══════════════════════════════════════════════════════════════════════╗",
            "║                                                                       ║",
            "║                                                                       ║",
            "║                     T E R M I N A T I O N   C O M P L E T E           ║",
            "║                                                                       ║",
            "║                                                                       ║",
            "╚═══════════════════════════════════════════════════════════════════════╝"
        };

            foreach (string line in exitFrame)
            {
                Console.WriteLine(line);
                Thread.Sleep(100);
            }

            // Flicker effect
            for (int i = 0; i < 5; i++)
            {
                Console.Clear();
                Thread.Sleep(200);
                foreach (string line in exitFrame)
                {
                    Console.WriteLine(line);
                }
                Thread.Sleep(200);
            }

            Console.ResetColor();
            Console.Clear();
            Thread.Sleep(500);
            Console.CursorVisible = true;
        }

        private static void DrawDigitalRain()
        {
            Console.Clear();
            Random random = new Random();
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            // Create arrays for the digital rain
            int[] rainDrops = new int[width];
            int[] rainSpeeds = new int[width];
            char[] rainChars = new char[width];
            ConsoleColor[] rainColors = new ConsoleColor[width];

            // Initialize the rain properties
            for (int i = 0; i < width; i++)
            {
                rainDrops[i] = random.Next(-20, 0); // Start drops above the screen
                rainSpeeds[i] = random.Next(1, 4);  // Variable speeds
                rainChars[i] = GetRandomMatrixChar(random);
                rainColors[i] = GetRainColor(random, i % 3); // Create color patterns
            }

            // Run the animation for a few seconds
            DateTime endTime = DateTime.Now.AddSeconds(5); // Extended to 5 seconds for more impact

            // Dramatic intro effect
            Console.ForegroundColor = ConsoleColor.White;
            string message = "SYSTEM BREACH IN PROGRESS";
            int centerX = (width - message.Length) / 2;
            int centerY = height / 2;

            Console.SetCursorPosition(centerX, centerY);
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(50);
            }
            Thread.Sleep(500);
            Console.Clear();

            while (DateTime.Now < endTime)
            {
                // Don't clear the entire screen each time for trailing effect

                for (int i = 0; i < width; i++)
                {
                    // Randomly change characters for flickering effect
                    if (random.Next(0, 100) < 15)
                    {
                        rainChars[i] = GetRandomMatrixChar(random);
                    }

                    // First erase the previous position if visible
                    if (rainDrops[i] >= 0 && rainDrops[i] < height)
                    {
                        Console.SetCursorPosition(i, rainDrops[i]);
                        Console.Write(" ");
                    }

                    // Move the raindrop based on its speed
                    if (random.Next(0, 100) < 80) // Higher probability for movement
                    {
                        rainDrops[i] += rainSpeeds[i];
                    }

                    // Draw the current position
                    if (rainDrops[i] >= 0 && rainDrops[i] < height)
                    {
                        // Position cursor and draw rain character
                        Console.SetCursorPosition(i, rainDrops[i]);

                        // Vary intensity based on position - leading character is brightest
                        if (rainDrops[i] - rainSpeeds[i] < 0)
                        {
                            Console.ForegroundColor = ConsoleColor.White; // Head of stream is white
                            Console.Write(rainChars[i]);
                        }
                        else
                        {
                            // Create fading trail effect
                            Console.ForegroundColor = rainColors[i];
                            Console.Write(rainChars[i]);

                            // Draw trail with dimming effect
                            for (int trail = 1; trail <= 5; trail++)
                            {
                                int trailPos = rainDrops[i] - trail;
                                if (trailPos >= 0 && trailPos < height)
                                {
                                    Console.SetCursorPosition(i, trailPos);
                                    // Dim the character as the trail gets longer
                                    if (trail >= 3)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                    }
                                    Console.Write(GetRandomMatrixChar(random, 30)); // 30% chance to change character in trail
                                }
                            }
                        }
                    }

                    // Reset when drop goes off screen with varied probabilities
                    if (rainDrops[i] >= height && random.Next(0, 100) < 10)
                    {
                        rainDrops[i] = random.Next(-5, 0);
                        rainSpeeds[i] = random.Next(1, 4);
                        rainChars[i] = GetRandomMatrixChar(random);
                        rainColors[i] = GetRainColor(random, i % 3);
                    }
                }

                // Add occasional bright flashes
                if (random.Next(0, 100) < 5)
                {
                    int flashX = random.Next(0, width);
                    int flashY = random.Next(0, height);
                    Console.SetCursorPosition(flashX, flashY);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("@");
                }

                Thread.Sleep(50);
            }

            // Dramatic exit effect
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            message = "SECURITY PROTOCOL ACTIVATED";
            centerX = (width - message.Length) / 2;
            Console.SetCursorPosition(centerX, centerY);
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(30);
            }
            Thread.Sleep(800);
            Console.Clear();
        }

        // Helper method to generate random Matrix-style characters
        private static char GetRandomMatrixChar(Random random, int changeProb = 100)
        {
            if (random.Next(0, 100) < changeProb)
            {
                // Mix of numbers, letters, and special symbols for more variety
                string charSet = "01αβγδεζηθικλμνξο日本語ｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃｦｧｨｩｪｫｬｭｮｯABCDEF+-*/=$#@!?<>";
                return charSet[random.Next(0, charSet.Length)];
            }

            // If no change, return space
            return ' ';
        }

        // Helper method to get varied colors
        private static ConsoleColor GetRainColor(Random random, int pattern)
        {
            // Create different color patterns based on the index pattern
            if (random.Next(0, 100) < 80) // Most characters are green
            {
                return pattern == 0 ? ConsoleColor.Green :
                       pattern == 1 ? ConsoleColor.DarkGreen :
                       ConsoleColor.DarkCyan;
            }
            else
            {
                // Occasional different colors for variety
                return random.Next(0, 100) < 50 ? ConsoleColor.Cyan : ConsoleColor.DarkYellow;
            }
        }
    }
}
