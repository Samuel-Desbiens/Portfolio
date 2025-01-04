#include "stdafx.h"
#include "PasswordBox.h"


PasswordBox::PasswordBox(const std::string& fontName, unsigned int fontSize, unsigned int nbCharMax, const std::string& backgroundPath)
  : Textbox(fontName, fontSize, nbCharMax, backgroundPath)
{
}


PasswordBox::~PasswordBox()
{
}

const std::string PasswordBox::GetText() const
{
  return std::string(notEncryptedText.length(), '*');
}

void PasswordBox::SetText(const std::string & t)
{
  notEncryptedText = t;

  Textbox::SetText(GetText());
}

bool PasswordBox::OnTextEntered(const sf::Event::TextEvent & e)
{
  if (IsEnabled() && HasFocus())
  {
    switch (e.unicode)
    {
    case 0x8:
      if (GetText().length() > 0)
      {
        notEncryptedText.erase(notEncryptedText.length()-1,1);        
        SetText(notEncryptedText);
        break;
      }
    default:
    {
      char newChar = (char)e.unicode;
      if (newChar >= ' ')
      {
        // Vous pourriez faire beaucoup mieux ici pour gérer les caractères  non affichables
        if (GetText().length() < numCharMax)
        {
          notEncryptedText += newChar;
          //std::string t = GetText() + newChar;
          //Textbox::SetText(notEncryptedText);
          SetText(notEncryptedText);
        }

      }

    }
    break;
    }
    NotifyObservers(sf::Event::TextEntered);
  }

  return HasFocus();
}


const std::string PasswordBox::GetNotEcryptedText() const
{
  return notEncryptedText;
}
