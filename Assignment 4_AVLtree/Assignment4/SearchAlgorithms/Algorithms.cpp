#include "Algorithms.h"

using namespace std;

int Algorithms::linearSearch(int butts[], int sizeOfArray, int searchTerm)
{
	int i = 0;

	if (searchTerm > sizeOfArray || searchTerm < 1)
	{
		return -1;
	}

	while (i < sizeOfArray && (butts[i] != searchTerm))
	{
		i++;
	}


	return i;
}

int Algorithms::binarySearch(int butts[], int lower, int higher, int searchTerm)
{

	int index = (lower + higher) / 2;

	while ((butts[index] != searchTerm) && (lower <= higher))
	{
		if (butts[index] > searchTerm)               // If the number is > key, ..
		{                                                       // decrease position by one.
			higher = index - 1;
		}
		else
		{                                                        // Else, increase position by one.
			lower = index + 1;
		}
		index = (lower + higher) / 2;
	}
	if (lower <= higher)
	{
		return index;
	}

	return -1;
}
