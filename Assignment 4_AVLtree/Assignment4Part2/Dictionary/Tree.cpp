#include "Tree.h"
#include <string>
#include <iostream>
#include <iomanip>

using namespace std;

//Constructor
Tree::Tree()
{
	root = NULL;
}

//Destructor
Tree::~Tree()
{
	clearTree(root);
}

//Deletes all the nodes that branch from the input node - recursively.
void Tree::clearTree(Node *node)
{
	if (node != NULL)
	{
		clearTree(node->right);
		clearTree(node->left);
		delete node;
	}
}

//Inserts a new node into the tree (maintains balance)
void Tree::insert(Node *node) 
{
	Node *temp, *previous, *parent;

	//node->data = input;
	temp = root;
	previous = NULL;
	parent = NULL;

	//Make sure tree isn't empty
	if (root == NULL)
	{
		root = node;
		return;
	}

	//If tree isn't empty then search for a location to place new node
	while(temp != NULL)
	{
		previous = temp;

		if (temp->balance != '=')
		{
			parent = temp;
		}

		if (node->data.compare(temp->data) < 0)
		{
			temp = temp->left;
		}
		else
		{
			temp = temp->right;
		}
	}

	//Temp is now an empty leaf and parent points to the last ouf of balance node
	node->parent = previous;
	if (node->data.compare(previous->data) < 0)
	{
		previous->left = node;
	}
	else
	{
		previous->right = node;
	}

	//Rebalance the tree
	restoreBalance(parent, node);
}

//Method call to restore balance between the parent node and the new node inserted into the tree.
void Tree::restoreBalance(Node *parent, Node *node)
{
	//If the parent node is null because the tree is already balanced
	if (parent == NULL)
	{
		if (node->data.compare(root->data) < 0)
		{
			root->balance = 'L';
		}
		else
		{
			root->balance = 'R';
		}
		//Re-adjust the balance of all nodes from the new node until the root
		changeBalance(root, node);
	}
	
	
	//Insert new node as opposite leaf of parent's current balance
	else if(((parent->balance == 'L') && (node->data.compare(parent->data) > 0)) || ((parent->balance == 'R') && (node->data.compare(parent->data) < 0)))
	{
		//Re-adjust the balance of all the nodes from the new node until the parent
		changeBalance(parent, node);
	}

	
	//Insert the new node as the same side leaf as the balance of the parent node - in this case the right hand side.
	else if ((parent->balance == 'R') && (node->data.compare(parent->right->data) > 0))
	{
		//reset the parents balance
		parent->balance = '=';
		//do a single left-sided rotation on the parent
		rotateLeft(parent); 
		//Re-adjust the balance of all the nodes from the new node until the parent's parent
		changeBalance(parent->parent, node);
	}

	//Insert the new node as the same side leaf as the balance of the parent node - in this case the left hand side.
	else if ((parent->balance == 'L') && (node->data.compare(parent->left->data) < 0))
	{
		//reset the parents balance
		parent->balance = '=';
		//do a single right-sided rotation on the parent
		rotateRight(parent);
		//Re-adjust the balance of all the nodes from the new node until the parent's parent
		changeBalance(parent->parent, node);
	}

	//Insert the new node as the right child of the left leaf of the parent.
	else if ((parent->balance == 'L') && (node->data.compare(parent->left->data) > 0))
	{
		//Do a double right-hand rotation for the parent to fix balance
		rotateLeft(parent->left);
		rotateRight(parent);
		//Re-adjust the balance of all the nodes from the new node until the parent
		adjustLR(parent, node);
	}

	//Insert the new node as the left child of the right leaf of the parent.
	else
	{
		//Do a double left-handed rotation for the parent to fix balance
		rotateRight(parent->right);
		rotateLeft(parent);
		adjustRL(parent, node);
	}

}

//Changes the balance factor in all the nodes, starting from the starting node's parent up to but not including the final node in the branch.
void Tree::changeBalance(Node *final, Node *start)
{
	//Setting the starting point for the balance as the start node's parent.
	Node *temp = start->parent;
	while (temp != final && temp != NULL)
	{
		if (start->data.compare(temp->data) < 0)
		{
			temp->balance = 'L';
		}
		else
		{
			temp->balance = 'R';
		}
		temp = temp->parent;
	}
}

//Perform a single left-sided rotation around the node - node's parent will become the left child of node, and node's left child will become the parent's right child.
void Tree::rotateLeft(Node *node)
{
	//Temporarily store node's right child
	Node *temp = node->right;

	//Move temp's left child to be the right child of node
	node->right = temp->left;

	//If the left child exists, then reset the left child's parent
	if (temp->left != NULL)
	{
		temp->left->parent = node;
	}

	//If node was the root, make temp the new root
	if (node->parent == NULL)
	{
		root = temp;
		temp->parent = NULL;
	}

	//If node was the left child of it's parent, make temp the new left child
	else if (node->parent->left == node)
	{
		node->parent->left = temp;
	}

	//Otherwise make temp the new right child
	else
	{
		node->parent->right = temp;
	}

	//Move node to the left child of temp, and reset node's parent.
	temp->left = node;
	temp->parent = node->parent;
	node->parent = temp;

}


//Perform a single right-sided rotation around the node - node's parent will become the right child of node, and node's right child will become the parent's left child.
void Tree::rotateRight(Node *node)
{
	//Temporarily store node's left child
	Node *temp = node->left;

	//Move temp's right child to be the left child of node
	node->left = temp->right;

	//If the right child exists, then reset the right child's parent
	if (temp->right != NULL)
	{
		temp->right->parent = node;
	}

	//If node was the root, make temp the new root
	if (node->parent == NULL)
	{
		root = temp;
		temp->parent = NULL;
	}

	//If node was the left child of it's parent, make temp the new left child
	else if (node->parent->left == node)
	{
		node->parent->left = temp;
	}

	//Otherwise make temp the new right child
	else
	{
		node->parent->right = temp;
	}

	//Move node to the left child of temp, and reset node's parent.
	temp->right = node;
	temp->parent = node->parent;
	node->parent = temp;

}

//Adjusts the balance of the nodes between node just inserted, and final.
void Tree::adjustLR(Node *final, Node *node)
{
	if (final == root)
	{
		final->balance = '=';
	}
	else if (node->data.compare(final->parent->data) < 0)
	{
		final->balance = 'R';
		changeBalance(final->parent->left, node);
	}
	else
	{
		final->balance = '=';
		final->parent->left->balance = 'L';
		changeBalance(final, node);
	}
}

//Adjusts the balance of the nodes between node just inserted, and final.
void Tree::adjustRL(Node *final, Node *node)
{
	if (final == root)
	{
		final->balance = '=';
	}
	else if (node->data.compare(final->parent->data) > 0)
	{
		final->balance = 'L';
		changeBalance(final->parent->right, node);
	}
	else
	{
		final->balance = '=';
		final->parent->right->balance = 'R';
		changeBalance(final, node);
	}
}

void Tree::testPrint(Node *node, int indent)
{
	if (node != NULL)
	{
		testPrint(node->right, indent + 5);

		cout << setw(indent) << node->data << endl;

		testPrint(node->left, indent + 5);
	}
}

void Tree::printTree(Node *node, int indent)
{
	if (node != NULL)
	{
		printTree(node->right, indent + 5);

		cout << setw(indent) << node->data << endl;

		printTree(node->left, indent + 5);
	}
}

size_t Levenshtein(const std::string &s1, const std::string &s2)
{
	const size_t m(s1.size());
	const size_t n(s2.size());

	if (m == 0) return n;
	if (n == 0) return m;

	size_t *costs = new size_t[n + 1];

	for (size_t k = 0; k <= n; k++) costs[k] = k;

	size_t i = 0;
	for (std::string::const_iterator it1 = s1.begin(); it1 != s1.end(); ++it1, ++i)
	{
		costs[0] = i + 1;
		size_t corner = i;

		size_t j = 0;
		for (std::string::const_iterator it2 = s2.begin(); it2 != s2.end(); ++it2, ++j)
		{
			size_t upper = costs[j + 1];
			if (*it1 == *it2)
			{
				costs[j + 1] = corner;
			}
			else
			{
				size_t t(upper<corner ? upper : corner);
				costs[j + 1] = (costs[j]<t ? costs[j] : t) + 1;
			}

			corner = upper;
		}
	}

	size_t result = costs[n];
	delete[] costs;

	return result;
}


string Tree::search(string search)
{
	int testing = 100;
	string _temp = "";
	Node *temp = root;

	testing = Levenshtein(search, temp->data);

	while (temp != NULL)
	{
		if (Levenshtein(search, temp->data) <= testing)
		{
			testing = Levenshtein(search, temp->data);
			_temp = temp->data;
		}

		if (search.compare(temp->data) < 0)
		{
			temp = temp->left;
		}
		else if (search.compare(temp->data) > 0)
		{
			temp = temp->right;
		}
		else if (search.compare(temp->data) == 0)
		{
			return search;
		}

	}

	return _temp;
}



ostream& operator<<(ostream &output, Tree &tree)
{
	//tree.printTree(output, tree.root, 0);

	return output;
}