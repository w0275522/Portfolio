#include "Tree.h"
#include <iostream>
#include <fstream>
#include <conio.h>


using namespace std;

Node *createNode(string input);

Tree *createDictionary();



Node *createNode(string input)
{
	Node *temp = new Node();
	temp->data = input;
	temp->left = NULL;
	temp->right = NULL;
	temp->parent = NULL;
	temp->balance = '=';
	return temp;
}

Tree *createDictionary()
{
	string temp;
	Tree *_temp = new Tree();
	try {
		ifstream dictionary("dictionary.txt");
		while (getline(dictionary, temp))
		{
			_temp->insert(createNode(temp));
		}
	}
	catch (...)
	{
		cout << "Error opening dictionary file.";
	}
	return _temp;
}

void spellcheck(Tree *tree)
{
	string catchFile;
	try
	{
		ifstream passage("mispelled.txt");
		getline(passage, catchFile);
	}
	catch (...)
	{

	}
	string output[2000];
	int counter = 0;
	string temp = "";
	string catchC = "";
	for (char &singleC : catchFile)
	{
		if (singleC != ' ' && singleC != '\"' && singleC != '(' && singleC != ')' && singleC != '#' && singleC != '&' && singleC != '.' && singleC != ',' && singleC != '1' && singleC != '2' && singleC != '3')
		{
			temp += tolower(singleC);
		}
		if (singleC == ' ' || singleC == '\"' || singleC == '(' || singleC == ')' || singleC == '#' || singleC == '&' || singleC == '.' || singleC == ',' || singleC == '1' || singleC == '2' || singleC == '3')
		{
			output[counter] = temp;
			counter++;
			temp = "";
		}
	}

	cout << "Incorrect words: " << endl;
	for (int i = 0; i < counter; i++)
	{
		if (output[i].compare(tree->search(output[i])) != 0 && output[i].compare("") != 0)
		{
			cout << output[i] << " is mispelled." << endl;
		}
	}

}


void main()
{
	Tree *newTree = createDictionary();
	newTree->printTree(newTree->root, 15);
	spellcheck(newTree);


	_getch();
}
