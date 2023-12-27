import styled from "styled-components";
import { ProductTableOperations } from "../features/products/ProductOperations";
import { Pagination } from "./Pagination";
import { useProductsContext } from "../features/products/ProductsProvider";
import { useSearchParams } from "react-router-dom";
import { Search } from "./Search";

export const Header = () => {
  const productsContext = useProductsContext();
  const [searchParams] = useSearchParams();
  const pageSize = searchParams.get("pageSize") || "5";

  return (
    <StyledHeader>
      <p>SGS Prueba Tecnica</p>
      <Search />
      <ProductTableOperations />
      <Pagination count={productsContext.totalCount} pageSize={+pageSize} />
    </StyledHeader>
  );
};

const StyledHeader = styled.header`
  background-color: var(--color-grey-0);
  padding: 1.2rem 4.8rem;
  border-bottom: 1px solid var(--color-grey-100);
`;
