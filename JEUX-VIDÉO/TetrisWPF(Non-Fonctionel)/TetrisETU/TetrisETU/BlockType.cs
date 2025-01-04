using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
  public enum BlockType
  {
    None,
    Frozen,
    
    // **
    // **
    Square,
    
    // *
    // *
    // *
    // *
    Bar,
    
    //  *
    // ***
    T,
    
    // *
    // *
    // **
    L,
    
    // *
    // *
    //**
    J,
    
    //  *
    // **
    // *
    S,
    
    // *
    // **
    //  *
    Z
  }
}
