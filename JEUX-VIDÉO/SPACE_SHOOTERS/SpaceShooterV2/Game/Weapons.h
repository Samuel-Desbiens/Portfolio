#pragma once
class Weapons
{
public:
	//Construteur de weapons.
	Weapons(bool active);
	//Déstructeur de weapons.
	~Weapons();
	//Fonction permettant à l'arme de tirer.
	void Fire();
protected:
private:
	//Nombre d'update nécessaire avant de tirer un autre coup.
	int reload;
	//Si L'arme est active (arme qui vas tirer).
	bool active;
};

