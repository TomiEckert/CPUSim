# Microinstruction list

- [Microinstruction list](#microinstruction-list)
  - [RegisterToRegister](#registertoregister)
  - [MemoryRead](#memoryread)
  - [MemoryWrite](#memorywrite)
  - [Increment](#increment)
  - [Decrement](#decrement)
  - [Add](#add)
  - [Subtract](#subtract)
  - [Multiply](#multiply)
  - [Divide](#divide)
  - [IoReadInt](#ioreadint)
  - [IoWriteInt](#iowriteint)
  - [Set](#set)
  - [Decode](#decode)

## RegisterToRegister

### `RegisterToRegister(name, from, to)`

Transfers bit from one register to another.

|Name|Type|Description|
|---|---|---|
|`name`|`string`|Name of the microinstruction.|
|`from`|`string`|Name of the source register.|
|`to`|`string`|Name of the destination register.|

---

### `RegisterToRegister(name, from, to, fromStart, toStart, length)`

Transfers bit from one register to another and specifies what sequence of bits to transfer.

|Name|Type|Description|
|---|---|---|
|`name`|`string`|Name of the microinstruction.|
|`from`|`string`|Name of the source register.|
|`to`|`string`|Name of the destination register.|
|`fromStart`|`int`|Start index of the source register.|
|`toStart`|`int`|Start index of the destination register.|
|`length`|`int`|Length of the sequence.|

#### Example

The following microinstruction `ir(9-16)-ax` transfers bits from `ir` to `ax`.

```csharp
/*
 * ir(9-16)-ax          name
 * ir                   source register
 * ax                   destination register
 * 8                    skip first 8 bits from ir
 * 8                    skip first 8 bits in ax
 * 8                    transfer 8 bits
 */
RegisterToRegister("ir(9-16)-ax", "ir", "ax", 8, 8, 8)
/*
ir  1111111111111111    ->  1111111111111111

ax  0000000000000000    ->  0000000011111111
*/
```

---

## MemoryRead

### `MemoryRead(name, address, data, length)`

Reads a sequence of bits from the memory.

|Name|Type|Description|
|---|---|---|
|`name`|`string`|Name of the microinstruction.|
|`address`|`string`|Name of the memory address register.|
|`data`|`string`|Name of the memory data register.|
|`length`|`int`|Length of bit sequence to read.|

---

## MemoryWrite

### `MemoryWrite(name, address, data)`

Writes a sequence of bits to the memory.

|Name|Type|Description|
|---|---|---|
|`name`|`string`|Name of the microinstruction.|
|`address`|`string`|Name of the memory address register.|
|`data`|`string`|Name of the memory data register.|

---

## Increment

`Increment(name, register, number)`

Increments the registers value by the specified amount.

|Name|Type|Description|
|---|---|---|
|`name`|`string`|Name of the microinstruction.|
|`register`|`string`|Name of the register.|
|`number`|`int`|Number to increase the register's value with.|

---

## Decrement

`Decrement(name, register, number)`

Decrements the registers value by the specified amount.

|Name|Type|Description|
|---|---|---|
|`name`|`string`|Name of the microinstruction.|
|`register`|`string`|Name of the register.|
|`number`|`int`|Number to decrease the register's value with.|

---

## Add

### `Add(name, source1, source2, destination)`

Adds two values together and stores the result in the destination register.

|Name|Type|Description|
|---|---|---|
|`name`|`string`|Name of the microinstruction.|
|`source1`|`string`|Name of the first register.|
|`source2`|`string`|Name of the second register.|
|`destination`|`string`|Name of the destination register.|

---

## Subtract

### `Subtract(name, source1, source2, destination)`

Subtract two values from each other and stores the result in the destination register.

|Name|Type|Description|
|---|---|---|
|`name`|`string`|Name of the microinstruction.|
|`source1`|`string`|Name of the first register.|
|`source2`|`string`|Name of the second register.|
|`destination`|`string`|Name of the destination register.|

---

## Multiply

### `Multiply(name, source1, source2, destination)`

Multiplies two values and stores the result in the destination register.

|Name|Type|Description|
|---|---|---|
|`name`|`string`|Name of the microinstruction.|
|`source1`|`string`|Name of the first register.|
|`source2`|`string`|Name of the second register.|
|`destination`|`string`|Name of the destination register.|

---

## Divide

### `Divide(name, source1, source2, destination)`

Divides two values and stores the result in the destination register.

|Name|Type|Description|
|---|---|---|
|`name`|`string`|Name of the microinstruction.|
|`source1`|`string`|Name of the first register.|
|`source2`|`string`|Name of the second register.|
|`destination`|`string`|Name of the destination register.|

---

## IoReadInt

### `IoReadInt(name, register)`

Reads from the command line. If the input cannot be converted into int throws an exception.

|Name|Type|Description|
|---|---|---|
|`name`|`string`|Name of the microinstruction.|
|`register`|`string`|Name of the register.|

---

## IoWriteInt

### `IoWriteInt(name, register)`

Writes to the command line.

|Name|Type|Description|
|---|---|---|
|`name`|`string`|Name of the microinstruction.|
|`register`|`string`|Name of the register.|

---

## Set

### `Set(name, register, number)`

Sets the specified register to the provided number.

|Name|Type|Description|
|---|---|---|
|`name`|`string`|Name of the microinstruction.|
|`register`|`string`|Name of the register.|
|`number`|`int`|Number to set the register's value to.|

---

## Decode

### `Decode(name, register)`

Decodes the opcode from the current instruction and executes it.

|Name|Type|Description|
|---|---|---|
|`name`|`string`|Name of the microinstruction.|
|`register`|`string`|Name of the instruction register.|