#include "Algorithms.h"
#include <iostream>
#include <conio.h>
#include <string>
#include <time.h>

using namespace std;

void main()
{
	int butts[1000];

	//Create array of 1000 elements sorted
	for (int i = 1; i <= 1000; i++)
	{
		butts[i - 1] = i;
	}

	Algorithms test;
	int userInput;
	while (true)
	{
		cout << "Please enter a term to search for: ";
		cin >> userInput;

		int timer1 = clock();
		cout  << "Index of term " << userInput << ": " << test.linearSearch(butts, sizeof(butts) / 4, userInput) << endl;
		int timer2 = clock();
		cout << "Time taken to find with linear search: " << to_string(timer2 - timer1) << " second(s)." << endl;

		timer1 = clock();
		cout << "Index of term " << userInput << ": " << test.binarySearch(butts, 0, sizeof(butts) / 4, userInput) << endl;
		timer2 = clock();
		cout << "Time taken to find with binary search: " << to_string(timer2 - timer1) << " second(s)." << endl;

	}
	_getch();

}




