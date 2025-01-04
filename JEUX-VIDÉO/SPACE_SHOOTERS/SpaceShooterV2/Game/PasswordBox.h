#pragma once
#include "Textbox.h"
class PasswordBox :
  public Textbox
{
public:
  PasswordBox(const std::string& fontName, unsigned int fontSize, unsigned int nbCharMax, const std::string& backgroundPath = "");
  ~PasswordBox();
  virtual const std::string GetText() const override;
  virtual void SetText(const std::string& text) override;
  bool OnTextEntered(const sf::Event::TextEvent& e) override;
  const std::string GetNotEcryptedText() const;
private:
  std::string notEncryptedText;
};

