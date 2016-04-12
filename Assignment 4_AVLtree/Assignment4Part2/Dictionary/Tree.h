#pragma once
#ifndef _Node_H
#define _Node_H
#include <string>

using namespace std;

struct Node
{
	string data;
	Node *left;
	Node *right;
	Node *parent;
	char balance;
};

class Tree
{
public:
	Node *root;
	Tree();
	~Tree();
	void insert(Node *node);
	void restoreBalance(Node *parent, Node *newNode);
	void changeBalance(Node *final, Node *start);
	void rotateLeft(Node *node);
	void rotateRight(Node *node);
	void adjustLR(Node *final, Node *start);
	void adjustRL(Node *final, Node *start);
	void clearTree(Node *node);
	void testPrint(Node *node, int indent);
	void printTree(Node *node, int indent);
	string search(string search);
	friend ostream& operator<<(ostream &output, Tree &tree);
};


#endif