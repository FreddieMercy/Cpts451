   M S B A M L   `   `   `  ����  Cpts451_Project#    Cpts451_Project.ReviewOverAll   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  ReviewOverAll��5   	   $	��900��6   $
��1100��6,   .��5      � .+�5	   
   &   System.Windows.Controls.DataGrid  -   5          Name$ ReviewDG��5	      $��Stretch=�6%   $��Stretch=�6C   $��10,10,10,10q�6_      HeadersVisibility$ Column=�5
   #      CanUserReorderColumns .     CanUserAddRows .     CanUserResizeRows .     AutoGenerateColumns .  |�.     SelectionUnit$ FullRow=�5   5      SelectionMode$ Single=�6M   	   Columns	 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Review Date��6)      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6P      CellTemplate 5      �� 5      .R�5      �� ��6(   ��  �� Path$ revDate�5      5        5      $
 Stars��6)    . $
 Auto 6J    5      �� 5      .R�5      �� ��6(   �� $ Stars�5      5        5!      $
 Review Text��6)    . $
 auto 6P      MaxWidth$	 500Q�6]    5"      �� 5#      .R�5$      �� -   5        ��5$   %   �� $
 Text�6C   5%      5'        5)      $
 User ID��6)    . $
 Auto 6L    5*      �� 5+      .R�5,      �� ��6(   �� $ UserID�5-      5/        50      $
 Useful Votes��6)    . $
 Auto 6Q    51      �� 52      .R�53      �� ��6(   �� $
 Vote�54      56         ColumnHeaderStyle 58      �� 59       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5:      ��  �� Property � �� �� Value$	 5,0q�60   �� 5;       � ��$ 20��6/   5<         RowStyle 5>      �� 5?      )  #System.Windows.Controls.DataGridRow"  6   5@         	CellStyle 5B      �� 5C      *  $System.Windows.Controls.DataGridCell"  6   .�5D      ��  � ��
 �   �63   5E      5G      5J      5K      