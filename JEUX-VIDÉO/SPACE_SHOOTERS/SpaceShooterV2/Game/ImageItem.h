#pragma once
#include "Image.h"
class ImageItem :
  public Image
{
public:
  ImageItem(const std::string& path, const unsigned int scaleX = 1, const unsigned int scaleY = 1);
  ~ImageItem();
  ImageItem(const ImageItem & src);
  virtual bool CanHaveFocus() const override;

  void SetFocus(bool newFocus) override;
};

