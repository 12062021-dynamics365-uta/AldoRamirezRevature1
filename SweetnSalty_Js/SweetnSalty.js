let groupCount = 0; //Group count
let sweetCnt = 0; //sweet count
let saltyCnt = 0; //salty count
let sweetNSalty = 0; //sweet'nSalty count
let line = ""; //empty string

for(let i = 1; i <= 1000; i++) //Loop from 1 to 1000
{
    groupCount++; //Increment group count

    if (i % 3 == 0 && i % 5 == 0) //Multiples of both 3 and 5
    {
        line += "sweet'nSalty "; //Concat sweet'nSalty
        sweetNSalty++; //Increment sweet'nSalty count
    }
    else if (i % 3 == 0) //Multiples of only 3
    {
        line += "sweet "; //Concat sweet
        sweetCnt++; //Increment sweet count
    }
    else if (i % 5 == 0) //Multiples of only 5
    {
        line += "salty "; //Concat salty
        saltyCnt++; //Increment salty count
    }
    else //Not multiple of either 3 or 5
    line += `${i} `; //Concat number

    if (groupCount == 20) //Number of concats reaches 20
    {
        console.log(line); //Print string
        line = ""; //Reset string for new line
        groupCount = 0; //Reset group count
    }
}

console.log(`\nTotal sweet: ${sweetCnt}`); //Print sweet total
console.log(`Total salty: ${saltyCnt}`); //Print salty total
console.log(`Total sweet'nSalty: ${sweetNSalty}`); //Print sweet'nSalty total