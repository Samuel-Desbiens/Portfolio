#pragma once
class Weapons
{
public:
	//Construteur de weapons.
	Weapons(bool active);
	//D�structeur de weapons.
	~Weapons();
	//Fonction permettant � l'arme de tirer.
	void Fire();
protected:
private:
	//Nombre d'update n�cessaire avant de tirer un autre coup.
	int reload;
	//Si L'arme est active (arme qui vas tirer).
	bool active;
};

