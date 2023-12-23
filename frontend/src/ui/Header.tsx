import styled from "styled-components";
import { ProductTableOperations } from "../features/products/ProductOperations";
import { Pagination } from "./Pagination";
import { useProductsContext } from "../features/products/ProductsProvider";

export const Header = () => {
  const productsContext = useProductsContext();

  return (
    <StyledHeader>
      <p>SGS Prueba Tecnica</p>
      <ProductTableOperations />
      <Pagination count={productsContext.totalCount} />
    </StyledHeader>
  );
};

const StyledHeader = styled.header`
  background-color: var(--color-grey-0);
  padding: 1.2rem 4.8rem;
  border-bottom: 1px solid var(--color-grey-100);
`;
