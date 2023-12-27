import styled from "styled-components";

export type SelectOptions = {
  value: string;
  label: string;
};

export type SelectProps = {
  options: SelectOptions[];
  value: string;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
};

export const Select: React.FC<SelectProps> = ({
  options,
  value,
  onChange,
  ...props
}) => {
  return (
    <StyledSelect value={value} onChange={onChange} {...props}>
      {options.map((option) => (
        <option value={option.value} key={option.value}>
          {option.label}
        </option>
      ))}
    </StyledSelect>
  );
};

const StyledSelect = styled.select`
  font-size: 1.4rem;
  padding: 0.8rem 1.2rem;
  border: 1px solid
    ${(props) =>
      props.type === "white"
        ? "var(--color-grey-100)"
        : "var(--color-grey-300)"};
  border-radius: var(--border-radius-sm);
  background-color: var(--color-grey-0);
  font-weight: 500;
  box-shadow: var(--shadow-sm);
`;
