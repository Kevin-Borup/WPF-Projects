   M S B A M L   `   `   `  ����M  IWPF_VendingMachine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:WPF_VendingMachine.ViewsWPF_VendingMachine.Views  5      M+clr-namespace:WPF_VendingMachine.ViewModelsWPF_VendingMachine.ViewModels  5      -    'WPF_VendingMachine.Views.VendingMachine   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:WPF_VendingMachine.Views     5      :vm+clr-namespace:WPF_VendingMachine.ViewModels     5      $
��1033��5   5     � 	Resources  5	       �� DataType3   -WPF_VendingMachine.ViewModels.BottleViewModel �� Left
 �� Top��%  (��       5
      *, +)�� 6
   "  6   .R�5      �� .+�5      �� ��5      �� *ImageSource+��5      �� *
MaxWidth+��5      �� *	MaxHeight+ 5      �� *
Location.X+ 5      �� *
Location.Y+5      5      5      .��5      � .+�5   
   �� .+�5      �� .+�5      �� $��RightToLeft=�6    �� Orientation$ 
Horizontal=�69   .+�5      �� $	��277��6   $��Bottom=�6)   $	��200��6D   .+�5      �� -   5         ��Name$ SodaEnd��5       $��Stretch=�61   $	��208��6O   $��Bottom=�6\   $	��271��6w   $��../Graphics/EndSoda.png��6�   �� 5      
���   �$��1q�65   $	��108��6I   $��48��6U   $ 76��6a   $	 132��6r   ._�5      �� .+�5   "   �� $��Power��6(   $��Center=�68   �� 5   "   $ 
Horizontal=�6-   .+�5   &   	� $��On��62   $
��Left=�6�   $��5,0,0,0q�6�   ��6?   �� *PowerChangeSodaCommand+ d� 	IsChecked 6j   �� *SodaOn+6�   	� 5    &   $	��Off��62   $��Right=�6�   $��25,0,0,0q�6�   ��6@   �� *PowerChangeSodaCommand+ 6k   �� *	SodaOff+6�   5!   #   5"      5#      5$      �� 5%      -   5        $ ConveyorTop��5%      $��98��61   $��Bottom=�6>   $	��568��6Y   $"��../Graphics/ConveyorBelt.png��6e   �� 5'      -   5        $ ConveyorLiftTop��5'      $
��Left=�65   $	��222��6P   $	��Top=�6]   $	��115��6u   $"��../Graphics/ConveyorLift.png��6�   5(      �� 5)      $ 
Horizontal=�6    K� ZIndex$	 -10��66   .+�5*      �� .+�5+      �� $ 
Horizontal=�6%   .+�5,      �� $��Automatic: ��6$   $
��Left=�6:   $��Center=�6U   $��80,0,0,0q�6p   	� 5-      $��On��6q   $
��Left=�6~   $��Center=�6�   ��6*   �� *AutomaticCommand+ 6O   �� *AutomaticOn+6�   	� 5.      $	��Off��6r   $
��Left=�6�   $��Center=�6�   $��10,0,0,0q�6�   ��6*   �� *AutomaticCommand+ 6O   �� *AutomaticOff+6�   5/      �� 50      -   5        $ Producer��50       $
��Left=�62   $	��174��6M   $��Center=�6[   $	��295��6v   $��../Graphics/Producer.png��6�   �� 51      $ 
Horizontal=�6%   .+�52      �� $
��Soda��6%   $��Center=�6Z   $	��Top=�6w   $��	100,0,0,0q�6�   ��64   �� *SodaBottleCommand+6�   �� 53      $
��Beer��6%   $
��Left=�6Z   $	��Top=�6u   $��30,0,0,0q�6�   ��64   �� *BeerBottleCommand+6�   54      55      �� 56      $	 -10��6   .+�57      �� $��50��6N   $��50��6Y   $	 110��6e   ��6'   �� *CreatedBottles+6e   58      �� 59      -   5        $ ConveyorStart��59      $��65��63   $��Bottom=�6@   $	��486��6[   $"��../Graphics/ConveyorBelt.png��6g   $��Center=�6�   �� 5:      $	��277��6   .+�5;      �� -   5        $ Sorter��5;       $
��Left=�60   $	��267��6K   $	��Top=�6Y   $	��302��6q   $��../Graphics/Sorter.png��6}   �� 5<      
���   �$��1q�65   $	��108��6I   $��48��6U   $ 84��6a   $	 152��6r   ._�5=      �� $ 91��6)   $	 158��6:   .+�5>   "   �� $��Power��6(   $��Center=�68   �� 5?   "   $ 
Horizontal=�6-   .+�5@   &   	� $	��Off��62   $
��Left=�6�   $��5,0,0,0q�6�   ��6@   �� *PowerChangeSorterCommand+ 6m   �� *	SorterOff+6�   	� 5A   &   $��On��62   $��Right=�6�   $��25,0,0,0q�6�   ��6?   �� *PowerChangeSorterCommand+ 6l   �� *
SorterOn+6�   5B   #   5C      5D      5E      5F      �� 5G      $��RightToLeft=�6   $ 
Horizontal=�6:   .+�5H      �� $	��256��6   $	��209��6)   $��Bottom=�66   .+�5I      �� -   5        $ BeerEnd��5I       $
��Left=�61   $	��220��6L   $��Bottom=�6Y   $	��271��6t   $��../Graphics/EndBeer.png��6�   �� 5J      
���   �$��1q�65   $	��108��6I   $��48��6U   $ 74��6a   $	 144��6r   ._�5K      �� $	��105��6)   $��46��65   .+�5L   "   �� $��Power��6(   $��Center=�68   �� 5M   "   $ 
Horizontal=�6-   .+�5N   &   	� $��On��62   $
��Left=�6�   $��5,0,0,0q�6�   ��6?   �� *PowerChangeBeerCommand+ 6j   �� *BeerOn+6�   	� 5O   &   $	��Off��62   $��Right=�6�   $��25,0,0,0q�6�   ��6@   �� *PowerChangeBeerCommand+ 6k   �� *	BeerOff+6�   5P   #   5Q      5R      5S      �� 5T      -   5        $ ConveyorBottom��5T      $
��Left=�64   $��98��6O   $��Bottom=�6[   $	��568��6v   $"��../Graphics/ConveyorBelt.png��6�   �� 5V      -	   5        $ ConveyorLiftBottom��5V      $
��Left=�68   $	��236��6S   $	��Top=�6`   $	��103��6x   $"��../Graphics/ConveyorLift.png��6�   5W      5X      5Y      5`      5a      