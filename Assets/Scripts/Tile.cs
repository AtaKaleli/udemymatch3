using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int xIndex;
    public int yIndex;

    private Board m_board;



    public void Initialize(int xIndex, int yIndex, Board board)
    {
        this.xIndex = xIndex;
        this.yIndex = yIndex;
        
        m_board = board;
    }


    //dont forget to add a boxcollider2D and adjust the size of the x and y
    private void OnMouseDown()
    {
        if(m_board != null)
        {
            m_board.ClickTile(this);
        }
    }

    private void OnMouseEnter()
    {
        if (m_board != null)
        {
            m_board.DragToTile(this);
        }
    }

    private void OnMouseUp()
    {
        if (m_board != null)
        {
            m_board.ReleaseTile();
        }
    }
}
