#include "stdafx.h"
#include "NotImplementedException.h"


NotImplementedException::NotImplementedException()
  : std::logic_error("Function not yet implemented")
{
}


NotImplementedException::~NotImplementedException()
{
}
