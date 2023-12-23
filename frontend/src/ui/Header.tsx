import styled from "styled-components";
import { ProductTableOperations } from "../features/products/ProductOperations";

export const Header = () => {
  return (
    <StyledHeader>
      <p>SGS Prueba Tecnica</p>
      <ProductTableOperations />
    </StyledHeader>
  );
};

const StyledHeader = styled.header`
  background-color: var(--color-grey-0);
  padding: 1.2rem 4.8rem;
  border-bottom: 1px solid var(--color-grey-100);
`;
