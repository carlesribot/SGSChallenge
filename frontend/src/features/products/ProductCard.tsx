import styled from "styled-components";

type Product = {
  id: number;
  name: string;
  description: string;
  category: string;
  price: number;
  stock: number;
  imageUrl: string;
  isActive: boolean;
};

type Props = {
  product: Product;
};

export const ProductCard: React.FC<Product> = ({ product }: Props) => {
  return (
    <CardContainer>
      <ProductImage src={product.imageUrl} alt={product.name} />
      <ProductName>{product.name}</ProductName>
      <ProductName>{product.description}</ProductName>
      <ProductName>{product.category}</ProductName>
      <ProductPrice>${product.price}</ProductPrice>
      <ProductStatus>{product.isActive ? "Active" : "Inactive"}</ProductStatus>
    </CardContainer>
  );
};

const CardContainer = styled.div`
  border: 1px solid #ddd;
  border-radius: 8px;
  padding: 16px;
  margin: 16px;
  width: 200px;
`;

const ProductImage = styled.img`
  max-width: 100%;
  height: auto;
`;

const ProductName = styled.h3`
  margin-top: 8px;
  font-size: 16px;
`;

const ProductPrice = styled.p`
  margin-top: 8px;
  font-size: 14px;
  color: #888;
`;

const ProductStatus = styled.p`
  margin-top: 8px;
  font-size: 14px;
  color: #888;
`;
