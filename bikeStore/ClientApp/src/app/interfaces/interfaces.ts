
//
export interface IBikeCreation {
  bikeId: number;
  brand: string;
  model: string;
  isInStock: boolean;
  price: number;
  thumbBase64: string | ArrayBuffer;
  mainCategoryId: number;
  categoryId: number;
  colors: number[];
  sizes: number[];
  imgId?: number;
  thumbFileName?: string;
  junkColors: any[];
  junkSizes: any[];
}

export interface IBikesColors {
  id: number;
  bikeId: number;
  colorId: number;
}

export interface IBikesSizes {
  id: number;
  bikeId: number;
  sizeId: number;
}

export interface IBikeDto {
  bikeId: number;
  brand: string;
  colors: number[];
  sizes: number[];
  imgId: number;
  isInStock: boolean;
  junkColors: IBikesColors[];
  junkSizes: IBikesSizes[];
  mainCategoryId: number;
  categoryName: string;
  model: string;
  price: number;
  thumbBase64: string;
}

export interface IDictionaryConainer {
  id: any;
  value: string;
}

export interface IFile {
  fileId: number;
  fileName: string;
  fileType: string;
  fileCreateDt?: Date;
  base64String: string;
  isThumbnail: boolean;
}

export interface IBike {
  bikeId: number;
  brand: string;
  model: string;
  price: string;
  thumbBase64: string;
  imgId: number;
  sizes: Array<any>;
  colors: Array<any>;
}
