import { ReactNode, createContext, useContext, useState } from "react";

const DEFAULT_VALUE: ProductContext = {
  totalCount: 0,
  pageCount: 0,
  onSetValues: () => {},
};

interface ProductContext {
  totalCount: number;
  pageCount: number;
  onSetValues: (totalCount: number, pageCount: number) => void;
}

const ProductContextProvider = createContext<ProductContext>(DEFAULT_VALUE);

export const ProductsProvider = ({ children }: { children: ReactNode }) => {
  const [totalCount, setTotalCount] = useState<number>(0);
  const [pageCount, setPageCount] = useState<number>(0);

  const handleSetValues = (totalCount: number, pageCount: number) => {
    setTotalCount(totalCount);
    setPageCount(pageCount);
  };

  return (
    <ProductContextProvider.Provider
      value={{
        pageCount,
        totalCount,
        onSetValues: handleSetValues,
      }}
    >
      {children}
    </ProductContextProvider.Provider>
  );
};

export const useProductsContext = () => {
  const context = useContext(ProductContextProvider);

  if (context === undefined)
    throw new Error("ProductsContext was used outside of the ProductProvider");

  return context;
};
