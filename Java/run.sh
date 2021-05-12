mvn clean compile test
# java -jar /usr/java/lib/junit-platform-console-standalone-1.7.0.jar --class-path target/test-classes/ --scan-class-path
mvn exec:java -Dexec.mainClass="RTC.Scene"
