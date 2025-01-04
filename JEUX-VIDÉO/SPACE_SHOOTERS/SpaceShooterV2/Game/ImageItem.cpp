#include "stdafx.h"
#include "ImageItem.h"

 
ImageItem::ImageItem(const std::string& path, const unsigned int scaleX , const unsigned int scaleY )
  : Image(path, scaleX, scaleY)
{
  sprite.setColor(sf::Color(128, 128, 128, 255));
}

ImageItem::ImageItem(const ImageItem & src)
  : Image(src)
{
  texture.loadFromFile(src.texturePath);
  sprite.setTexture(texture);
  sprite.setScale(src.sprite.getScale());
  sprite.setColor(src.sprite.getColor());

}
ImageItem::~ImageItem()
{
}

bool ImageItem::CanHaveFocus() const
{
  return true;
}

void ImageItem::SetFocus(bool newFocus)
{
  Image::SetFocus( newFocus );
  sprite.setColor(newFocus?sf::Color::White:sf::Color(128,128,128,255));  
}
