namespace AdventOfCode2025.Days;

public class Day03 : IDay
{
    public int DayNumber => 3;

    public string SolvePart1(string input)
    {
        const int asciiCorrection = 48; // convert chars 1 to 9 to their decimal values
        var totalJoltage = 0;
        var banks = input.Split('\n');
        
        foreach (var bank in banks)
        {
            // find the first battery
            var batteryPrimeIndex = 0;
            var batteryPrimeValue = 0;
            
            for (var i = batteryPrimeIndex; i < bank.Length - 1; i++)
            {
                if (bank[i] - asciiCorrection > batteryPrimeValue)
                {
                    batteryPrimeIndex = i;
                    batteryPrimeValue = bank[i] - asciiCorrection;
                }
            }

            // find the second battery
            var batteryOmegaValue = 0;
            
            for (var i = batteryPrimeIndex + 1; i < bank.Length; i++)
            {
                if (bank[i] - asciiCorrection > batteryOmegaValue)
                {
                    batteryOmegaValue = bank[i] - asciiCorrection;
                }
            }
            
            totalJoltage += batteryPrimeValue * 10;
            totalJoltage += batteryOmegaValue;
        }

        return totalJoltage.ToString();
    }

    public string SolvePart2(string input)
    {
        const int asciiCorrection = 48; // convert chars 1 to 9 to their decimal values
        long totalJoltage = 0;
        var banks = input.Split('\n');
        const int bestBatterySequenceLength = 12;
        
        foreach (var bank in banks)
        {
            var activatedBatteries = 0;
            var countOfBatteriesToActivate = bestBatterySequenceLength;
            var activatedBatterySequence = new char[bestBatterySequenceLength];
            var currentBestBatteryIndex = 0;
            
            while (activatedBatteries < bestBatterySequenceLength)
            {
                var batteriesToInspect = bank.Length - currentBestBatteryIndex; // This is the reverse index
                
                // look at the next battery in the bank
                for (var i = currentBestBatteryIndex; i < bank.Length; i++)
                {
                    // is it stronger than the previous battery?
                    if (bank[i] > bank[currentBestBatteryIndex])
                    {
                        currentBestBatteryIndex = i;
                    }

                    batteriesToInspect--;
                    
                    // do we have enough batteries left in the bank for inspection? (ex., we have not yet found the first battery, but there are still more than 12 batteries left to inspect)
                    if (batteriesToInspect >= countOfBatteriesToActivate) continue;
                    
                    // we found the next best battery to activate
                    activatedBatterySequence[activatedBatteries] = bank[currentBestBatteryIndex];
                    activatedBatteries++;
                    countOfBatteriesToActivate--;
                    currentBestBatteryIndex++;
                    break;
                }
                
            }

            // add the joltage to the total
            long decimalPlace = 1;
            for (var i = activatedBatterySequence.Length - 1; i >= 0; i--)
            {
                totalJoltage += (activatedBatterySequence[i] - asciiCorrection) * decimalPlace;
                decimalPlace *= 10;
            }
        }

        return totalJoltage.ToString();
    }
}