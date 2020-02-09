import { Pipe, PipeTransform } from '@angular/core';
import { ICategory } from '../nav-menu/nav-menu.component';

@Pipe({
  name: 'menuCategoryPipe'
})
export class MenuCategoryPipePipe implements PipeTransform {

  transform(value: ICategory[], search: number): ICategory[] {
    if (!search) {
      return value;
    }
    return value.filter(item => item.mainCatId === search);
  }

}
